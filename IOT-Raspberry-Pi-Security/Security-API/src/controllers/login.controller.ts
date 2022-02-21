import {Controller, Post, UseGuards, Request, Body, BadRequestException, Put} from "@nestjs/common";
import {LocalAuthGuard} from "./auth/guards/local-auth.guard";
import {UserService} from "../services/user.service";
import {User} from "../models/user.schema";
import {AuthService} from "../services/auth.service";
import {JwtAuthGuard} from "./auth/guards/jwt-auth.guard";

/**
 * Class LoginController
 */
@Controller('login')
export class LoginController {

    /**
     * LoginController constructor
     * @param userService UserService - handles user
     * @param authService AuthService - handles authentication logic
     */
    constructor(private userService: UserService,
                private authService: AuthService) {
    }

    /**
     * POST request to /login
     * @param request @Request -
     */
    @UseGuards(LocalAuthGuard)
    @Post()
    getById(@Request() request): any {
        return this.authService.login(request.user);
    }

    /**
     * POST request to /login/register
     * @param dto @Body()
     * @return saved user
     */
    @Post(':register')
    post(@Body() dto: User) {
        // Look up for a user with this username
        return this.userService.findOne(dto.username).then(user => {

            // Do not create a user with a same username
            if (user) {
                throw new BadRequestException('Username already taken!');
            }

            // Create new user and immediately login in by returning access_token
            return this.userService.newUser(dto).then(user => {
                return this.authService.login(user);
            });
        });
    }

    /**
     * PUT request to /login/update
     * @param request @Request -
     */
    @UseGuards(JwtAuthGuard)
    @Put(':update')
    updateUser(@Request() request): any {
       return this.userService.updateUser( request.body );
    }
}
