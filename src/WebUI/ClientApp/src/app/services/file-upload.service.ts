import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, finalize, map, tap} from 'rxjs/operators';
import {BehaviorSubject, Observable, throwError} from "rxjs";
import {UploadFileCommand} from "../models/file";

class PaginatedList<T> {
}

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private data$? = new BehaviorSubject<PaginatedList<any>>(null);
  private data?: PaginatedList<any>;
  constructor(private httpClient: HttpClient) {
  }

  postFile(uploadfileCommand: UploadFileCommand): Observable<boolean> {
  /*  const endpoint = 'api/File/UploadFile';
    const formData: FormData = new FormData();
    console.log(uploadfileCommand, "pop")
    return this.httpClient
      .post(endpoint, uploadfileCommand)
      .pipe(map(() => {
        return true;
      }))*/

    const endpoint = 'api/File/UploadFile';
    const formData: FormData = new FormData();
    return this.httpClient
      .post(endpoint, uploadfileCommand)
      .pipe(
        tap((response: any) => {
          this.data$.next(this.data);
        }),
        catchError((error) => {
          //this.loading = false;
          return throwError(error);
        }),
        finalize(() => {
          // this.loading = false;
        })
      );

  }
}
