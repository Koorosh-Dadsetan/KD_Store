import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {

  constructor(
    private title: Title
  ) {
    this.title.setTitle("KD_Store - Counter");
  }

  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
