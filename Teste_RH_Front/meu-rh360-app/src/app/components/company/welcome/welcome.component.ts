import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CompanyFormComponent } from '../company-form/company-form.component';
import { NgbModal, NgbModalConfig, NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, NgbModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {
  userName: string = 'usuário';
  userId: string | null = null;
  isCompanyConfigured: boolean = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private modalConfig: NgbModalConfig
  ) {
    modalConfig.backdrop = 'static';
    modalConfig.keyboard = false;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const name = params.get('fullName');
      const id = params.get('userId');

      if (name) { this.userName = name; }
      if (id) { this.userId = id; } else { console.warn('Parâmetro userId não encontrado na rota.'); }
      this.isCompanyConfigured = false;
    });
  }

  startConfiguration(): void {
    if (!this.userId) {
      console.error('ID do usuário não disponível para configurar a empresa.');
      alert('Erro: ID do usuário não encontrado. Por favor, tente novamente fazer o login ou cadastro.');
      this.router.navigate(['/register']);
      return;
    }

    const modalRef = this.modalService.open(CompanyFormComponent, { size: 'lg' });
    modalRef.componentInstance.userId = this.userId;

    modalRef.result.then((result) => {
      if (result === 'companySaved') {
        this.isCompanyConfigured = true;


        this.router.navigate(['/company-details', this.userId]);
      }
    }).catch((reason) => {
    });
  }
}