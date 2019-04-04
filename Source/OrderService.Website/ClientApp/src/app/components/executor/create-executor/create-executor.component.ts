import { AuthService } from "./../../../services/auth.service";
import { ExecutorService } from "./../../../services/executor.service";
import { NewExecutor } from "./../../../models/newExecutor";
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Work } from "./../../../models/work";
import { WorkService } from "./../../../services/work.service";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { environment } from "./../../../../environments/environment";

@Component({
  selector: "app-create-executor",
  templateUrl: "./create-executor.component.html",
  styleUrls: ["./create-executor.component.css"]
})
export class CreateExecutorComponent implements OnInit {
  images: number[];
  works: Work[];
  executorForm: FormGroup;
  loading = false;
  executor: NewExecutor = new NewExecutor();
  private baseUrl: string = environment.baseUrl;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private executorService: ExecutorService,
    private workService: WorkService,
    private auth: AuthService
  ) {}

  afuConfig = {
    multiple: true,
    formatsAllowed: ".jpg,.png,.jpeg",
    maxSize: '5',
    uploadAPI: {
      url: `${this.baseUrl}/api/photos`
    },
    theme: 'dragNDrop',
    hideProgressBar: false,
    hideResetBtn: false,
    hideSelectBtn: false,
    replaceTexts: {
      selectFileBtn: 'Select',
      resetBtn: 'Reset',
      uploadBtn: 'Upload',
      dragNDropBox:
        'Drag photos of your work here or select files. Pictures will help you get more orders.',
      attachPinBtn: 'Attach Files...',
      afterUploadMsg_success: 'Successfully Uploaded !',
      afterUploadMsg_error: 'Upload Failed !'
    }
  };

  ngOnInit() {
    this.executorForm = this.formBuilder.group({
      organizationName: ['', Validators.required],
      description: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      workTypeId: ['']
    });

    this.workService.getWorks().subscribe(
      data => {
        this.works = data;
      },
      error => {}
    );
  }

  docUpload($event) {
    this.images = JSON.parse($event.responseText);
  }

  onSubmit() {
    this.loading = true;
    if (this.executorForm.valid) {
      this.executor = this.executorForm.value;
      this.executor.photos = this.images;
      this.executorService.createExecutor(this.executor).subscribe(
        async data => {
          await this.auth.refreshToken();
          this.router.navigate(['/executors']);
        },
        error => {
          this.loading = false;
        }
      );
    } else {
      return;
    }
  }
}
