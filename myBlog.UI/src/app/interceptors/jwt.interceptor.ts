import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from '../services/user.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private userService: UserService) {

  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const currentUser = this.userService.currentUserValue;
    const isLoggedIn = currentUser && currentUser.Token;
    const isApiUrl = request.url.startsWith(environment.webApi);

    if(isApiUrl && isLoggedIn){
      request= request.clone(
        {
          setHeaders:{
            Authorization:'Bearer ${currentUser.Token}'
          }
        }
      )
      return next.handle(request);
    }

    return next.handle(request);
  }
}
