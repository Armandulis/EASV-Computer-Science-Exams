import {PassportStrategy} from "@nestjs/passport";
import {Strategy} from "passport-local";
import {Injectable, UnauthorizedException} from "@nestjs/common";
import {AuthService} from "../../services/auth.service";

/**
 * Class LocalStrategy
 */
@Injectable()
export class LocalStrategy extends PassportStrategy(Strategy) {

    /**
     * LocalStrategy constrcutor
     * @param authService AuthService - handles Auth
     */
    constructor( private authService: AuthService )
    {
        // Local strategy requires no configuration
        super();
    }

    /**
     * Returns authenticated user or throws UnauthorizedException
     * @param username string - user's plain text username
     * @param password string - user's plain text password
     */
    async validate( username: string, password: string): Promise<any>
    {
        // Find authenticated user
        const user = await this.authService.validateUser( username, password );

        // If no authenticated user was found - throw exception
        if (!user )
        {
            throw new UnauthorizedException();
        }

        return user;
    }
}
