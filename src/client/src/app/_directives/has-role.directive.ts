import { Directive, Input, TemplateRef, ViewContainerRef, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Directive({
  selector: '[appHasRole]',
  standalone: true
})
export class HasRoleDirective { // remove ele from dom based on ....

  @Input() appHasRole: string[] = [];
  private accountService = inject(AccountService);
  private viewContainerRef = inject(ViewContainerRef);
  private templateRef = inject(TemplateRef);

  ngOnInit(): void {
    if (this.appHasRole.includes(this.accountService.role()))
      this.viewContainerRef.createEmbeddedView(this.templateRef) //  display the elements in the dom

    else
      this.viewContainerRef.clear(); //clear the viewContainerRef that removes  element from the DOM.

  }


}
