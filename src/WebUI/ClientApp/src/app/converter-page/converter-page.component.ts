import {Component, OnInit} from '@angular/core';
import {UploadFile, UploadFileCommand} from "../models/file";
import {FileUploadService} from "../services/file-upload.service";

@Component({
  selector: 'app-converter-page',
  templateUrl: './converter-page.component.html',
  styleUrls: ['./converter-page.component.scss']
})
export class ConverterPageComponent implements OnInit {
  // form: FormGroup;
  fileToUpload: File | null = null;
  file1: File = null;
  file2: File = null;
  loading = false;
  byColumn :number = 1
  done = false;
  constructor(
/*    private formBuilder: FormBuilder,*/
    private fileUploadService: FileUploadService
  ) {
  }

  ngOnInit(): void {
/*    this.form = this.formBuilder.group({
      file1: ["", []],
      file2: ["", []],
    })*/

  }



  onFilechange(event: any, fileNumber: number) {
    if (fileNumber == 1) {
      this.file1 = event.target.files[0]
    } else if (fileNumber == 2) {
      this.file2 = event.target.files[0]
    }

    console.log(this.file1);
    console.log(this.file2);
  }

  upload() {
    if(!(this.file1 && this.file2 && this.byColumn)){
      console.log("lracreq bolory")
      return null;
    }
    this.loading = true;
    let file1 = new UploadFile();
    let file2 = new UploadFile();
    file1.name = this.file1.name;
    file2.name = this.file2.name;
    let reader = new FileReader();
    let files = [file1, file2]
    let uploadedFiles = [this.file1, this.file2]

    let readFile = (index) => {
      if (index >= files.length) {
        console.log("FINISHED");
        let uploadFileCommand = new UploadFileCommand()
        uploadFileCommand.file1 = files[0]
        uploadFileCommand.file2 = files[1]
        uploadFileCommand.byColumn = this.byColumn;


        this.fileUploadService.postFile(uploadFileCommand).subscribe((res:any)=>{
          this.loading = false;
          if(res?.succeeded){
            this.done = true;
            setTimeout(() => {
             this.done = false;
            }, 3000);
          }

        })
        return
      }
      reader.onload = (e) => {

        files[index].fileBase64 = e.target.result;
        readFile(index + 1);
        /*if (this.file1) {
          reader.readAsDataURL(this.file2)
          //

        } else {
          alert("Please select a file first")
        }*/
      }
      reader.readAsDataURL(uploadedFiles[index]);


    }
    readFile(0);

  }

  setByColumn(event:any){
    this.byColumn = event?.target?.value;
  }
}
