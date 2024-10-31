import { Component, OnInit} from '@angular/core';
import { ExamsClient } from '../../../api/api-reference';
import { Chart, LinearScale, BarElement, BarController, Title, Tooltip,CategoryScale  } from 'chart.js'; 

@Component({
  selector: 'app-exam-statistics',
  templateUrl: './exam-statistics.component.html',
  styleUrls: ['./exam-statistics.component.css']
})
export class ExamStatisticsComponent implements OnInit{
  constructor(private service: ExamsClient) {}

  ngOnInit() {
    this.service.getExamStatistics().subscribe(statistics => {
      const canvas = document.getElementById('examStatisticsChart') as HTMLCanvasElement;
      const ctx = canvas?.getContext('2d');

      if (ctx) { 
        Chart.register(LinearScale, BarElement, BarController, Title, Tooltip, CategoryScale); 

        new Chart(ctx, {
          type: 'bar',
          data: {
            labels: ['Over 50%', 'Under 50%'],
            datasets: [{
              label: 'Percentage of Exams',
              data: [statistics.over50Percent, statistics.under50Percent],
              backgroundColor: [
                'rgba(75, 192, 192, 0.2)',
                'rgba(255, 99, 132, 0.2)'  
              ],
              borderColor: [
                'rgba(75, 192, 192, 1)', 
                'rgba(255, 99, 132, 1)'    
              ]
            }]
          }
        });
      }
    });
  }
  
}
