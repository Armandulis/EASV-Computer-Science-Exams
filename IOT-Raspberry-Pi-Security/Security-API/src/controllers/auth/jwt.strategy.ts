import {PassportStrategy} from "@nestjs/passport";
import {ExtractJwt, Strategy} from "passport-jwt";
import {Injectable} from "@nestjs/common";
import {UserService} from "../../services/user.service";

/**
 * Class JwtStrategy
 */
@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy) {

    /**
     * JwtStrategy constructor
     * Function super(); acts as jwt authentication method
     * @param userService UserService - Manages user
     */
    constructor( private userService: UserService ) {
        // This is actually where the real validation is happening
        super(
            {
                secretOrKey: 'IOT_EXAM_2021_RASP_SECURITY', // TODO: put in env
                jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
                ignoreExpiration: false,
            }
        );
    }

    /**
     * Returns payload with a JWT token and user's data
     * Does not actually validates anything, because validation already happened in the constructor (super();)
     * @param payload - json payload (our user)
     * @return {} - Json with JWT token
     */
    async validate(payload: any) {
        // Get user by id form user service
        return payload;
    }
}
