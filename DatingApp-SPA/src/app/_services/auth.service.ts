import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { mapToExpression } from '@angular/compiler/src/render3/view/util';
import {map} from 'rxjs/operators';
// tslint:disable-next-line: no-unused-expression
import {JwtHelperService} from '@auth0/angular-jwt';
import { from } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodeToken: any;
  currUser: User;
  photourl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotUrl = this.photourl.asObservable();

constructor(private http: HttpClient) { }

changeMemberPhoto(photourl: string) {
  this.photourl.next(photourl);
}
login(model: any) {
  return this.http.post(this.baseUrl + 'login', model)
  .pipe(map((response: any) => {
    const user = response;
    if (user) {
      localStorage.setItem('token', user.token);
      localStorage.setItem('user', JSON.stringify(user.user));
      this.currUser = user.user;
      this.decodeToken = this.jwtHelper.decodeToken(user.token);
      this.changeMemberPhoto(this.currUser.photoUrl);
    }
  }));
}



register(user: User)  {
return this.http.post(this.baseUrl + 'register', user);
}

loggeding() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}
}
