import {Injectable} from "@nestjs/common";
import {InjectModel} from "@nestjs/mongoose";
import {Model} from "mongoose";
import {User, UserDocument} from "../models/user.schema";

/**
 * Injectable class UserService
 */
@Injectable()
export class UserService {

    /**
     * UserService constructor
     */
    constructor(
        @InjectModel(User.name)
        private userModel: Model<UserDocument>,
    ) {

    }

    /**
     * Finds User by username
     * @param username - User's username
     * @return Promise<User> - found user
     */
    async findOne(username: string): Promise<User | undefined> {
        return this.userModel.findOne({username: username}).exec();
    }

    /**
     * Saves new user
     * @param user User - User that is about to be created
     * @return Promise<User> - Saved user
     */
    async newUser(user: User): Promise<User> {

        // Encrypt password
        const bcrypt = require('bcrypt');
        const saltRounds = 10;
        user.password = bcrypt.hashSync(user.password, saltRounds);
        user.sensorIds = [];

        // Save User
        const userModel = new this.userModel(user);
        return userModel.save();
    }

    /**
     * Updates user with new values
     * @param user User - User with new values
     * @return Promise<User> - Saved user
     */
    async updateUser(user: User) {
        // Update user
        return this.userModel.updateOne({username: user.username}, user).exec();
    }
}
