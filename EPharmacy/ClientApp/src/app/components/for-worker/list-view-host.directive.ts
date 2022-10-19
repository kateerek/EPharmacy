import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appListViewHost]'
})
export class ListViewHostDirective {

  constructor(public viewContainerRef: ViewContainerRef) { }

}
