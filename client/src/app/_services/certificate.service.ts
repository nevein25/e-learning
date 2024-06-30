import { Injectable, inject } from '@angular/core';
import jsPDF from 'jspdf';
import { Certificate } from '../_models/certificate';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CoursePurshased } from '../_models/coursesBought';

@Injectable({
  providedIn: 'root'
})
export class CertificateService {


  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  certifcate: Certificate | undefined;

  getCertifcateDate(courseId: number) {
    return this.http.get<Certificate>(`${this.baseUrl}certificates/${courseId}`).subscribe({
      next: res => {
        this.certifcate = res;
        console.log(res);
        this.generatePdfCertificate(this.certifcate);

      },
      error: error => console.log(error)
    });
  }

  generatePdfCertificate(certificate: Certificate) {
    const doc = new jsPDF('landscape', 'mm', 'a4');
    const completionDate = new Date(certificate.completionDate).toLocaleDateString('en-GB');

    // Add a background image (if you have one)
    //const img = new Image();
   // img.src = '/assets/logo/logo.png'; // Replace with your image path
    //img.onload = () => {
      //doc.addImage(img, 'JPEG', 0, 0, 297, 210);

      // Add logo (optional)
      const logo = new Image();
      logo.src = '/assets/logo/logo2.png'; // Replace with your logo path
      doc.addImage(logo, 'PNG', 10, 10, 50, 30);

      // Add Certificate Title
      doc.setFontSize(30);
      doc.setTextColor(40, 70, 100);
      doc.text('Certificate of Completion', 148, 50, { align: 'center' });

      // Add Student Name
      doc.setFontSize(22);
      doc.setTextColor(0, 0, 0);
      doc.text(`This certifies that`, 148, 90, { align: 'center' });
      doc.setFontSize(26);
      doc.text(`${certificate.studentName}`, 148, 110, { align: 'center' });

      // Add Course Name and Completion Date
      doc.setFontSize(18);
      doc.text(`has successfully completed the course`, 148, 130, { align: 'center' });
      doc.setFontSize(22);
      doc.text(`${certificate.courseName}`, 148, 150, { align: 'center' });
      doc.setFontSize(18);
      doc.text(`on ${completionDate}`, 148, 170, { align: 'center' });

      // Save the PDF
      doc.save('Certificate.pdf');
    // };
  }

  isStudentFinishedCourse(courseId: number){
    return this.http.get<any>(`${this.baseUrl}coursesPurshases/finished/${courseId}`);
  }
}
