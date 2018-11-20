import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { CustomValidators } from '../_classes/custom-validators';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  
  loginFormGroup: FormGroup;
  
  constructor(private authService: AuthService, private router: Router, private alertService: AlertService) {
    this.loginFormGroup = new FormGroup({
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
        Validators.minLength(8)]))
      });
      
    }
    
    ngOnInit() {
    }
    
    /// Login
    login = () => {
      this.authService.Login(this.loginFormGroup.value).subscribe((response: boolean) => {
        if(response == true){
          this.alertService.success("Successfully Loggedin!");
          this.router.navigate(['home']);
        }else{
          this.alertService.success("Invalid Username or password");
        }
      });
    }
    
    
  }
  