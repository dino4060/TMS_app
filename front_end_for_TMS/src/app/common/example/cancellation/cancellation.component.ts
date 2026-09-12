import { Component } from '@angular/core';
import { CancellationService } from '@platform/example/example';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-cancellation',
  imports: [ButtonModule],
  templateUrl: './cancellation.component.html',
  styleUrl: './cancellation.component.css',
})
export class CancellationComponent {

  constructor(
    private cancellationService: CancellationService
  ) { }

  makeWorkRequest(): void {
    // Logic to make a work request
    console.log('Making a work request...');

    this.cancellationService.makeRequest().subscribe({
      next: () => {
        console.log('Work request made successfully.');
      },
      error: (error) => {
        console.error('Error making work request:', error);
      }
    });
    
  }

  cancelWorkRequest(): void {
    // Logic to cancel a work request
    console.log('Cancelling a work request...');

    this.cancellationService.cancelRequest().subscribe({
      next: () => {
        console.log('Work request cancelled successfully.');
      },
      error: (error) => {
        console.error('Error cancelling work request:', error);
      }
    });
  }
}
