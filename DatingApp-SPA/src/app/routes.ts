import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './allMembers/lists/lists.component';
import { AuthGuard } from './_gaurds/auth.guard';
import { MemberDetailComponent } from './allMembers/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/memberdetail.resolver';
import { MemberListResolver } from './_resolvers/memberlist.resolver';
import { MemebrEditComponent } from './allMembers/memebr-edit/memebr-edit.component';
import { MemberEditResolver } from './_resolvers/memberedit.resolver';
import { PreventUnsaved } from './_gaurds/prevent-unsaved.guard';
import { ListsResolver } from './_resolvers/lists.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
export const appRoute: Routes = [
    { path: '', component: HomeComponent},
    { path: '', runGuardsAndResolvers: 'always', canActivate: [AuthGuard], children: [
    { path: 'members', component:  ListsComponent , resolve: {users: MemberListResolver}},
    { path: 'members/:id', component: MemberDetailComponent, resolve: {users: MemberDetailResolver}},
    { path: 'member/edit', component: MemebrEditComponent,  resolve: {users: MemberEditResolver}, canDeactivate: [PreventUnsaved]},
    { path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResolver}},
    { path: 'lists', component: MembersComponent , resolve: {users: ListsResolver}},
    ]},
    { path: '**', redirectTo: '', pathMatch: 'full'},
];
