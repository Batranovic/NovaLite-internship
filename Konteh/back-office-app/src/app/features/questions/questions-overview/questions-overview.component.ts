import { Component } from '@angular/core';
import { QuestionsClient } from '../../../api/api-reference';

@Component({
  selector: 'app-questions-overview',
  templateUrl: './questions-overview.component.html',
  styleUrl: './questions-overview.component.css'
})
export class QuestionsOverviewComponent {
  apiResponse: string | undefined;
  constructor(private questionClient: QuestionsClient ) {

  }
/*
  helloWorlds() {
    this.httpClient.get("https://localhost:7184/questions/hello").subscribe(resp => {
      this.apiResponse = JSON.stringify(resp);
    });
  }*/

  helloWorld() {
    this.questionClient.getHello().subscribe({
      next: (response) => {
      
        const reader = new FileReader();
        reader.onload = () => {
          const text = reader.result as string;
          const json = JSON.parse(text);
          this.apiResponse = json.message;  
        };
        reader.readAsText(response.data);  
      },
    
    });
  }
  
}
