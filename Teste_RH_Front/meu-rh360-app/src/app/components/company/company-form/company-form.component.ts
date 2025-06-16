import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

interface Address {
  zipCode: string;
  address: string;
  neighborhood: string;
  state: string;
  city: string;
  addressComplement: string;
}

interface CompanyUpdateData {
  userId: string;
  companyType: string;
  companyName: string;
  documentNumber: string;
  administratorName: string;
  administratorDocumentNumber: string;
  email: string;
  mobilePhone: string;
  address: Address;
}

@Component({
  selector: 'app-company-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './company-form.component.html',
  styleUrls: ['./company-form.component.scss']
})
export class CompanyFormComponent implements OnInit {
  @Input() userId: string | null = null;
  companyForm!: FormGroup;
  loading = false;
  submitted = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    public activeModal: NgbActiveModal
  ) { }

  ngOnInit(): void {
    if (!this.userId) {
      console.error('CompanyFormComponent: userId não foi fornecido.');
      this.errorMessage = 'Erro: ID do usuário não disponível para configurar a empresa.';
     
    }

    this.companyForm = this.formBuilder.group({
      companyType: ['', Validators.required],
      companyName: ['', Validators.required],
      documentNumber: ['', [Validators.required, Validators.pattern(/^\d{14}$/)]],
      administratorName: ['', Validators.required],
      administratorDocumentNumber: ['', [Validators.required, Validators.pattern(/^\d{11}$/)]],
      email: ['', [Validators.required, Validators.email]],
      mobilePhone: ['', Validators.required],
      address: this.formBuilder.group({
        zipCode: ['', [Validators.required, Validators.pattern(/^\d{8}$/)]],
        address: ['', Validators.required],
        neighborhood: ['', Validators.required],
        state: ['', Validators.required],
        city: ['', Validators.required],
        addressComplement: ['']
      })
    });
  }

  get f() { return this.companyForm.controls; }
  get a() { return (this.companyForm.get('address') as FormGroup).controls; }

  onZipCodeChange(event: Event) {
    const zipCode = (event.target as HTMLInputElement).value;
    if (zipCode && zipCode.length === 8) {
      this.lookupAddressByZipCode(zipCode);
    }
  }

  lookupAddressByZipCode(zipCode: string): void {
   
    const VIACEP_URL = `https://viacep.com.br/ws/${zipCode}/json/`;
    this.http.get<any>(VIACEP_URL).subscribe({
      next: (data) => {
        if (!data.erro) {
          this.companyForm.get('address')?.patchValue({
            address: data.logradouro,
            neighborhood: data.bairro,
            state: data.uf,
            city: data.localidade
          });
          this.errorMessage = null;
        } else {
          this.errorMessage = 'CEP não encontrado.';
          this.clearAddressFields();
        }
      },
      error: (err) => {
        console.error('Erro ao consultar CEP:', err);
        this.errorMessage = 'Erro ao consultar CEP. Tente novamente.';
        this.clearAddressFields();
      }
    });
  }

  clearAddressFields(): void {
    this.companyForm.get('address')?.patchValue({
      address: '',
      neighborhood: '',
      state: '',
      city: '',
      addressComplement: ''
    });
  }

  onSubmit(): void {
    this.submitted = true;
    this.errorMessage = null;
    this.successMessage = null;

    if (this.companyForm.invalid) {
      console.log('Formulário da empresa inválido:', this.companyForm.errors);
      this.errorMessage = 'Por favor, preencha todos os campos obrigatórios corretamente.';
      return;
    }

    if (!this.userId) {
      this.errorMessage = 'Erro interno: ID do usuário não disponível.';
      return;
    }

    this.loading = true;

    const companyData: CompanyUpdateData = {
      userId: this.userId,
      companyType: this.f['companyType'].value,
      companyName: this.f['companyName'].value,
      documentNumber: this.f['documentNumber'].value,
      administratorName: this.f['administratorName'].value,
      administratorDocumentNumber: this.f['administratorDocumentNumber'].value,
      email: this.f['email'].value,
      mobilePhone: this.f['mobilePhone'].value,
      address: {
        zipCode: this.a['zipCode'].value,
        address: this.a['address'].value,
        neighborhood: this.a['neighborhood'].value,
        state: this.a['state'].value,
        city: this.a['city'].value,
        addressComplement: this.a['addressComplement'].value
      }
    };

    const API_URL = 'https://localhost:7197/api/Users/company';

    this.http.put(API_URL, companyData).subscribe({
      next: (response) => {
        console.log('Dados da empresa atualizados com sucesso!', response);
        this.successMessage = 'Empresa configurada com sucesso!';
        this.loading = false;
       
        this.activeModal.close('companySaved');
      },
      error: (error) => {
        console.error('Erro ao atualizar dados da empresa:', error);
        this.loading = false;
        this.errorMessage = error.error?.message || 'Erro ao configurar a empresa. Tente novamente.';

       
        if (error.status === 400 && error.error?.errors) {
          this.errorMessage = Object.values(error.error.errors).flat().join('; ');
        }
      }
    });
  }

  closeModal(): void {
    this.activeModal.dismiss('cancel');
  }
}