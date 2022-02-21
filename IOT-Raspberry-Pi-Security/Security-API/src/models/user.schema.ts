import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';

/**
 * User Model
 */
export type UserDocument = User & Document;

@Schema()
export class User{
    @Prop()
    id: number;

    @Prop()
    name: string;

    @Prop()
    username: string;

    @Prop()
    password: string;

    @Prop()
    sensorIds: string[];
}
export const UserSchema = SchemaFactory.createForClass(User);
