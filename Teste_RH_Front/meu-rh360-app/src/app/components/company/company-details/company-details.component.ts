import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

interface Address {
  zipCode: string;
  address: string;
  neighborhood: string;
  state: string;
  city: string;
  addressComplement: string;
}

interface CompanyDetails {
  companyType: string;
  companyName: string;
  documentNumber: string;
  administratorName: string;
  administratorDocumentNumber: string;
  email: string;
  mobilePhone: string;
  address: Address;
}

interface UserResponse {
  id: string; 
  insertDate: string;
  updateDate: string;
  fullName: string;
  email: string;
  company: CompanyDetails; 
}

@Component({
  selector: 'app-company-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.scss']
})
export class CompanyDetailsComponent implements OnInit {
  companyData: CompanyDetails | null = null;
  loading: boolean = true;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const userId = params.get('userId');

      if (userId) {
        this.fetchUserDetailsWithCompany(userId); 
      } else {
        this.errorMessage = 'ID do usuário não encontrado para carregar os detalhes da empresa.';
        this.loading = false;
        console.error(this.errorMessage);
      }
    });
  }

  
  fetchUserDetailsWithCompany(userId: string): void {
    this.loading = true;
    this.errorMessage = null;

    const API_URL = `https://localhost:7197/api/Users/${userId}`; 

    this.http.get<UserResponse>(API_URL).subscribe({
      next: (data) => {
        if (data && data.company) { 
          this.companyData = data.company; 
        } else {
          this.errorMessage = 'Dados da empresa não encontrados para este usuário.';
          console.warn('Resposta da API não contém dados da empresa:', data);
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar dados do usuário/empresa:', error);
        this.loading = false;
        this.errorMessage = error.error?.message || 'Erro ao carregar os dados da empresa. Tente novamente.';
      }
    });
  }
}