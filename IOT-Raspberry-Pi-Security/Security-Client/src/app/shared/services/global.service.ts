import {BehaviorSubject} from 'rxjs';
import {Injectable} from "@angular/core";
import {User} from "../models/user";

/**
 * Class GlobalService
 */
@Injectable({providedIn: 'root'})
export class GlobalService {

  public tokenValue = new BehaviorSubject(this.theToken);

  /**
   * Sets the token in local storage and BehaviorSubject
   * @param token string - The token
   */
  set theToken(token: string | null) {

    // This will make sure to tell every subscriber about the change.
    this.tokenValue.next(token);

    // Replace old access token with a new one
    sessionStorage.removeItem('access_token');
    if (token !== null) {
      sessionStorage.setItem('access_token', token);
    }
  }

  /**
   * Gets the access_token from local storage
   * @return string|null - currently stored access_token
   */
  get theToken(): string | null {
    return sessionStorage.getItem('access_token');
  }

  /**
   * Gets the user from local storage
   * @return User|null - currently stored user
   */
  public getUser(): User | null {

    const userJson = sessionStorage.getItem('user');

    if (this.theToken === null || userJson === null) {
      return null;
    }

    return JSON.parse( userJson );
  }

  /**
   * Sets the user to local storage
   * @return User|null - Logged in user
   */
  public setUser(user: User|null): void{
    // Replace old access token with a new one
    sessionStorage.removeItem('user');
    if (user !== null) {
      sessionStorage.setItem('user', JSON.stringify( user ) );
    }
  }
}
