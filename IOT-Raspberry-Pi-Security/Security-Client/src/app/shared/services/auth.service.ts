import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Auth} from "../models/auth";
import {environment} from "../../../environments/environment";
import {Observable} from 'rxjs';
import {User} from "../models/user";

@Injectable({
  providedIn: 'root'
})

/**
 * Class AuthService
 */
export class AuthService {

  /**
   * AuthService constructor
   * @param http HttpClient - send requests to the API
   */
  constructor(private http: HttpClient) {
  }

  /**
   * Sends a POST request to the API to login user
   * @param auth Auth - DTO for user's credentials
   * @return {access_token: string} - Json access token
   */
  public login(auth: Auth): Observable<any> {
    return this.http.post(environment.apiURL + '/login', auth);
  }

  /**
   * Sends a POST request to the API to Register user
   * @param auth Auth - DTO for user's credentials
   * @return {access_token: string} - Json access token
   */
  public register(auth: Auth): Observable<any> {
    return this.http.post(environment.apiURL + '/login/register', auth);
  }

  /**
   * Sends a PUT request to the API to update user
   * @param user User - User with new values
   * @return
   */
  updateUser(user: User): Observable<Object> {
    return this.http.put(environment.apiURL + '/login/update', user, { headers: AuthService.getBearerTokenHeader() } );
  }

  /**
   * Returns HTTPHeader with authorization token bearer
   * Token received form local storage
   */
  public static getBearerTokenHeader() : HttpHeaders  {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': "Bearer " + sessionStorage.getItem( 'access_token' )
    });
  }
}
