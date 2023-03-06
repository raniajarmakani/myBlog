import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserCreate } from '../Models/User/user-create.models';
import { UserLogin } from '../Models/User/user-login.model';
import { User } from '../Models/User/user.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSubject$: BehaviorSubject<User>

  constructor(
    private http: HttpClient
  ) { 
    this.currentUserSubject$ = new BehaviorSubject<User>(JSON.parse(localStorage.getItem("myBlog-currentUser")!));
  }

  login(model: UserLogin) : Observable<User>  {
    return this.http.post<User>(`${environment.webApi}/Account/login`, model).pipe(
      map((user : User) => {

        if (user) {
          localStorage.setItem('myBlog-currentUser', JSON.stringify(user));
          this.setCurrentUser(user);
        }

        return user;
      })
    )
  }

  register(model: UserCreate) : Observable<User> {
    return this.http.post<User>(`${environment.webApi}/Account/register`, model).pipe(
      map((user : User) => {

        if (user) {
          localStorage.setItem('myBlog-currentUser', JSON.stringify(user));
          this.setCurrentUser(user);
        }

        return user;
      })
    )
  }

  setCurrentUser(user: User) {
    this.currentUserSubject$.next(user);
  }

  public get currentUserValue(): User {
    return this.currentUserSubject$.value;
  }

  public givenUserIsLoggedIn(username: string) {
    return this.isLoggedIn() && this.currentUserValue.username === username;
  }

  public isLoggedIn() {
    const currentUser = this.currentUserValue;
    const isLoggedIn = !!currentUser && !!currentUser.token;
    return isLoggedIn;
  }

  logout() {
    localStorage.removeItem('myBlog-currentUser');
    this.currentUserSubject$.next(null);
  }
}