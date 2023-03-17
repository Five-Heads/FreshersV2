import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, tap } from "rxjs";
import { User } from "./models/user.model";
import { environment } from "src/environment/environment";

export class AuthResponseModel {
    constructor(public token: string) { }
}

@Injectable({ 
    providedIn: 'root' 
})
export class AuthService {
    user = new BehaviorSubject<User | null>(null);
    private apiUrl = environment.apiUrl;

    constructor(
        private http: HttpClient,
        private router: Router
    ) { }

    checkIsUserAuthenticatedOnStart(): boolean {
        // TODO: logic
        const userData = localStorage.getItem('userData');
        if (!userData) {
            return false;
        }

        this.user.next(JSON.parse(userData));
        return true;
    }

    register(userName: string, password: string) {
        return this.http.post<AuthResponseModel>(`${this.apiUrl}/auth/register`, {
            userName: userName,
            password: password
        })
            .pipe(
                tap(model => {
                    this.handleAuthSuccess(new User('1', userName, model.token));
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
                    this.handleAuthSuccess(new User('1', userName, model.token));
                })
            )
    }

    logOut() {
        this.user.next(null);
        this.router.navigate(['/auth']);
        localStorage.removeItem('userData');
    }

    private handleAuthSuccess(user: User) {
        this.user.next(user);
        localStorage.setItem('userData', JSON.stringify(user));
    }
}