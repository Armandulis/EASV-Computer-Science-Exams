import {Injectable} from "@nestjs/common";
import {InjectModel} from "@nestjs/mongoose";
import {Model} from "mongoose";
import {Auth, AuthDocument} from "../models/auth.schema";
import {UserService} from "./user.service";
import {JwtService} from "@nestjs/jwt"

/**
 * Injectable class AuthService
 */
@Injectable()
export class AuthService {

    /**
     * AuthService constructor
     * @param authModel Model -
     * @param userService UserService - Handles User
     * @param jwtService JwtService - handles Jwt authentication system
     */
    constructor(
        @InjectModel(Auth.name)
        private authModel: Model<AuthDocument>,
        private userService: UserService,
        private jwtService: JwtService
    ) {

    }

    /**
     * Finds and validates user
     * @param username string - user's plain text username
     * @param password string - user's plain text password
     */
    async validateUser(username: string, password: string) {
        // Finds the user
        const user = await this.userService.findOne(username);

        // We will have to decrypt the saved password
        const bcrypt = require('bcrypt');

        // Make sure that user's password match what user provided
        if (user && bcrypt.compareSync(password, user.password)) {
            return user;
        }
    }

    /**
     * Login user by returning user's access_token (bearer)
     * @param user any - User that is trying to login
     */
    async login(user: any) {
        const payload = {username: user.username, sub: user.id};

        // Finds the user
        const foundUser = await this.userService.findOne(user.username);

        return {
            access_token: this.jwtService.sign(payload),
            user: foundUser
        }
    }
}
