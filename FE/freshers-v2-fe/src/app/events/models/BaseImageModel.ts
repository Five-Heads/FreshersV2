import { BlurredImageModel } from "./BlurredImageModel";

export class BaseImageModel {
    public Id: number = 0;
    public Object: string = '';
    public Base64Image: string = '';
    public BlurredImages: BlurredImageModel[] = [];
}