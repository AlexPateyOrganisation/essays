import {Component, inject, OnInit} from '@angular/core';
import {NzFormControlComponent, NzFormDirective, NzFormItemComponent, NzFormLabelComponent} from 'ng-zorro-antd/form';
import {FormsModule, NonNullableFormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {NzCardComponent} from 'ng-zorro-antd/card';
import {NzIconDirective} from 'ng-zorro-antd/icon';
import {NzButtonComponent} from 'ng-zorro-antd/button';
import {AuthorRequest} from '../../core/contracts/authorRequest.model';
import {EssayRequest} from '../../core/contracts/essayRequest.model';
import {EssayService} from '../services/essay.service';
import {Router} from '@angular/router';
import {NzInputDirective} from 'ng-zorro-antd/input';

@Component({
  selector: 'app-essay-create',
  imports: [
    NzFormDirective,
    FormsModule,
    NzFormItemComponent,
    NzFormLabelComponent,
    NzFormControlComponent,
    ReactiveFormsModule,
    NzCardComponent,
    NzIconDirective,
    NzButtonComponent,
    NzInputDirective
  ],
  templateUrl: './essay-create.html',
  styleUrl: './essay-create.scss'
})
export class EssayCreate implements OnInit {

  private readonly router = inject(Router);
  private readonly essayService = inject(EssayService);
  private formBuilder = inject(NonNullableFormBuilder);
  private nextAuthorControlId: number = 0;

  validateForm = this.formBuilder.record({
    title: this.formBuilder.control('', [Validators.required]),
    body: this.formBuilder.control('', [Validators.required])
  });

  listOfAuthorControls: Array<{
    id: number;
    firstNameControlInstance: string;
    lastNameControlInstance: string;
    dateOfBirthControlInstance: string;
  }> = [];

  ngOnInit(): void {
    this.onAddAuthorField();
  }

  onAddAuthorField(e?: MouseEvent) {
    e?.preventDefault();

    const authorControl = {
      id: this.nextAuthorControlId,
      firstNameControlInstance: `authorFirstName${this.nextAuthorControlId}`,
      lastNameControlInstance: `authorLastName${this.nextAuthorControlId}`,
      dateOfBirthControlInstance: `authorDateOfBirth${this.nextAuthorControlId}`
    };

    this.nextAuthorControlId++;

    const index = this.listOfAuthorControls.push(authorControl);

    this.validateForm.addControl(
      this.listOfAuthorControls[index - 1].firstNameControlInstance,
      this.formBuilder.control('', [Validators.required])
    );

    this.validateForm.addControl(
      this.listOfAuthorControls[index - 1].lastNameControlInstance,
      this.formBuilder.control('', [Validators.required])
    );

    this.validateForm.addControl(
      this.listOfAuthorControls[index - 1].dateOfBirthControlInstance,
      this.formBuilder.control('', [Validators.required])
    );
  }

  onDeleteAuthorField(e?: MouseEvent) {
    e?.preventDefault();

    if (this.listOfAuthorControls.length > 1) {
      const lastElement = this.listOfAuthorControls.pop();

      this.validateForm.removeControl(lastElement!.firstNameControlInstance!);
      this.validateForm.removeControl(lastElement!.lastNameControlInstance!);
      this.validateForm.removeControl(lastElement!.dateOfBirthControlInstance!);
    }
  }

  async onSubmit(): Promise<void> {

    const createEssayButton = document.getElementById("createEssayButton")!;
    createEssayButton.setAttribute("disabled", "true");

    if (this.validateForm.valid) {

      let authorRequests: AuthorRequest[] = [];

      for (let i = 0; i < this.listOfAuthorControls.length; i++) {
        authorRequests.push({
          firstName: this.validateForm.value[this.listOfAuthorControls[i].firstNameControlInstance]!,
          lastName: this.validateForm.value[this.listOfAuthorControls[i].lastNameControlInstance]!,
          dateOfBirth: this.getDateOnly(new Date(this.validateForm.value[this.listOfAuthorControls[i].dateOfBirthControlInstance]!))
        });
      }

      const essayRequest: EssayRequest = {
        title: this.validateForm.value["title"]!,
        body: this.validateForm.value["body"]!,
        authors: authorRequests
      };

      const essayResponse = await this.essayService.createEssay(essayRequest);

      if (essayResponse) {
        await this.router.navigateByUrl(`essays/${essayResponse.id}`);
      }

    } else {
      createEssayButton.removeAttribute("disabled");

      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  getDateOnly(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');

    return `${year}-${month}-${day}`;
  }
}
