import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, take, tap } from "rxjs";
import { User } from "./models/user.model";
import { environment } from "src/environment/environment";
import { JwtHelperService } from '@auth0/angular-jwt';

export class AuthResponseModel {
    constructor(
        public id: string,
        public facultyNumber: string,
        public userName: string,
        public token: string
    ) { }
}

@Injectable({ 
    providedIn: 'root' 
})
export class AuthService {
    user = new BehaviorSubject<User | null>(null);
    private apiUrl = environment.apiUrl;

    constructor(
        private http: HttpClient,
        private router: Router,
        private jwtHelper: JwtHelperService
    ) { }

    checkIsUserAuthenticatedOnStart(): boolean {
        const userData = localStorage.getItem('userData');

        if (!userData) {
            return false;
        }

        const user = JSON.parse(userData);

        if (this.jwtHelper.isTokenExpired()) {
            localStorage.removeItem('userData');
            return false;
        }

        this.user.next(user);
        return true;
    }

    register(userName: string, facultyNumber: string, email: string, password: string) {
        return this.http.post<AuthResponseModel>(`${this.apiUrl}/auth/register`, {
            userName: userName,
            facultyNumber: facultyNumber,
            email: email,
            password: password
        })
            .pipe(
                tap(model => {
                    this.handleAuthSuccess(new User(model.id, model.userName, model.facultyNumber, model.token));
                })
            )
    }

    login(userName: string, password: string) {
        return this.http.post<AuthResponseModel>(`${this.apiUrl}/auth/login`, {
            userName: userName,
            password: password
        })
            .pipe(
                tap(model => {
                    this.handleAuthSuccess(new User(model.id, model.userName, model.facultyNumber, model.token));
                }),
                take(1)
            )
    }

    logOut() {
        this.user.next(null);
        this.router.navigate(['/']);
        localStorage.removeItem('userData');
    }

    private handleAuthSuccess(user: User) {
        user.role = this.jwtHelper.decodeToken(user.token).role;
        this.user.next(user);
        localStorage.setItem('userData', JSON.stringify(user));
    }
}