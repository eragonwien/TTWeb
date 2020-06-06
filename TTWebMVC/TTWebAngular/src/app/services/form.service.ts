import { concat } from 'rxjs';
import { FormGroup, NgForm } from '@angular/forms';
import { Injectable } from '@angular/core';

@Injectable()
export class FormService {
  constructor() {}
  displayError(
    formGroup: FormGroup,
    ngForm: NgForm,
    field: string,
    includeErrors: string[] = [],
    excludeErrors: string[] = []
  ) {
    if (ngForm.submitted || formGroup.controls[field].touched) {
      let includeError = false;
      let excludeError = true;
      if (includeErrors && includeErrors.length > 0) {
        includeErrors.forEach((i) => {
          if (formGroup.controls[field].hasError(i)) {
            includeError = true;
          }
        });
      }

      if (excludeErrors && excludeErrors.length > 0) {
        excludeErrors.forEach((e) => {
          if (formGroup.controls[field].hasError(e)) {
            excludeError = false;
          }
        });
      }

      return includeError && excludeError;
    }
    return false;
  }

  getModelStateErrors(modelStateError: object) {
    if (!modelStateError) {
      return [];
    }
    return [].concat.apply([], Object.values(modelStateError));
  }
}
