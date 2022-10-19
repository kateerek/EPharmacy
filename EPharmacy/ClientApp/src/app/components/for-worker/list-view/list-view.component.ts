import { Component, OnInit, ViewChild, ComponentFactoryResolver, forwardRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ListViewHostDirective } from '../list-view-host.directive';

@Component({
  selector: 'app-list-view',
  templateUrl: './list-view.component.html',
  styleUrls: ['./list-view.component.css']
})
export class ListViewComponent implements OnInit {
  @ViewChild(forwardRef(() => ListViewHostDirective)) listViewHost: ListViewHostDirective;
  componentInstance: any;

  constructor(private route: ActivatedRoute, private factoryResolver: ComponentFactoryResolver) { }

  ngOnInit() {
    this.route.data.subscribe(data => this.loadComponent(data['componentType']));
  }

  loadComponent(componentType: any) {
    let componentFactory = this.factoryResolver.resolveComponentFactory(componentType);

    let viewContainerRef = this.listViewHost.viewContainerRef;
    viewContainerRef.clear();

    let listComponentRef = viewContainerRef.createComponent(componentFactory);
    this.componentInstance = listComponentRef.instance;
  }

}
