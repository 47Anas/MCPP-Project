import { UploaderMode, UploaderStyle, UploaderType } from './uploader.enum';

export class ImageUploaderConfig {

  style: UploaderStyle;
  mode: UploaderMode;
  type: UploaderType;

  constructor(style: UploaderStyle, mode: UploaderMode, type: UploaderType) {

    this.style = style;
    this.mode = mode;
    this.type = type;
  }
}
