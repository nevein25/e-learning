import { Component, inject, input } from '@angular/core';
import { CertificateService } from '../_services/certificate.service';
import { Certificate } from '../_models/certificate';

@Component({
  selector: 'app-download-certificate',
  standalone: true,
  imports: [],
  templateUrl: './download-certificate.component.html',
  styleUrl: './download-certificate.component.css'
})
export class DownloadCertificateComponent {

  id = input.required<any>(); // p to c, the new way

  certificateService = inject(CertificateService);

  generateCertificate() {
    this.certificateService.getCertifcateDate(this.id());
  }
}
