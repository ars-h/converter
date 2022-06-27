export class UploadFile {
  name: string;
  fileBase64: string | ArrayBuffer;

}


export class UploadFileCommand{
  file1:UploadFile;
  file2:UploadFile;
  byColumn:number
}
