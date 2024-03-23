import { Directive, TemplateRef, ViewContainerRef, inject } from '@angular/core';

@Directive({
  selector: '[appGrantedTo]'
})
export class GrantedToDirective {
 /* private _rbacService = inject(RbacService);
  private _templateRef = inject(TemplateRef);
  private _viewContainer = inject(ViewContainerRef);
  private _user!: User;
  private _roleOrPermission!: string;*/
  
  constructor() { }

}
