import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';

/**
 * Motion Model
 */
export type MotionDocument = Motion & Document;

@Schema()
export class Motion {
    @Prop()
    sensorId: string;

    @Prop()
    measurementTime: Date;

    @Prop()
    base64Picture: string;

    @Prop()
    fullSavedPicturePath: string;

    @Prop()
    downloadUrl: string;
}
export const MotionSchema = SchemaFactory.createForClass(Motion);
