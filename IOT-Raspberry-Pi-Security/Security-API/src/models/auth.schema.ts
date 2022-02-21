import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';

/**
 * Auth Model
 */
export type AuthDocument = Auth & Document;

@Schema()
export class Auth{
    @Prop()
    id: number;

    @Prop()
    username: string;
}
export const AuthSchema = SchemaFactory.createForClass(Auth);
