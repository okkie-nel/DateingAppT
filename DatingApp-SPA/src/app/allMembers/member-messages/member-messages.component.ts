import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Message } from 'src/app/_models/message';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
@Input() recpientId: number;
messages: Message[];
  constructor(private userservice: UserService, private authservice: AuthService, 
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadMessages();
  }

    loadMessages() {
      this.userservice.getMessageThread(this.authservice.decodeToken.nameid, this.recpientId).subscribe(messages => {
        this.messages = messages;
      }, error => {
        this.alertify.error(error);
      });
    }
}
