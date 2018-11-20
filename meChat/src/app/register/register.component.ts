import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { CustomValidators } from '../_classes/custom-validators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerFormGroup: FormGroup;
  
  constructor(private authService: AuthService, private router: Router, private fb: FormBuilder) {
    this.registerFormGroup = this.fb.group({
      name: new FormControl(null, Validators.compose([Validators.required])),
      username: new FormControl(null, Validators.compose([Validators.required])),
      password: new FormControl(null, Validators.compose([
        // 1. Password Field is Required
        Validators.required,
        // 2. check whether the entered password has a number
        CustomValidators.patternValidator(/\d/, { hasNumber: true }),
        // 3. check whether the entered password has upper case letter
        CustomValidators.patternValidator(/[A-Z]/, { hasCapitalCase: true }),
        // 4. check whether the entered password has a lower-case letter
        CustomValidators.patternValidator(/[a-z]/, { hasSmallCase: true }),
        // 5. check whether the entered password has a special character
        CustomValidators.patternValidator(/[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/, { hasSpecialCharacters: true }),
        // 6. Has a minimum length of 8 characters
        Validators.minLength(8)])),
        conf_password: new FormControl(null, Validators.compose([Validators.required]))
    },
    {
      validator: CustomValidators.passwordMatchValidator
    });
  }

  ngOnInit() {
  }

  // register
  register = () => {
    this.authService.RegisterUser(this.registerFormGroup.value).subscribe((response: any) => {
      alert("User registered successfully! Please login to MeChat");
      this.router.navigate(['login']);
    }, error => {
      alert("Failed to register member!");
    });
  }

}
