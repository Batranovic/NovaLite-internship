import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionsClient } from '../../../api/api-reference';
import { Chart, LinearScale,  DoughnutController, Title, Tooltip, CategoryScale, ArcElement } from 'chart.js';

@Component({
  selector: 'app-question-statistics',
  templateUrl: './question-statistics.component.html',
  styleUrls: ['./question-statistics.component.css']
})
export class QuestionStatisticsComponent implements OnInit {
  questionId: number | null = null;

  constructor(private questionClient: QuestionsClient, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.questionId = +id;
        this.loadQuestionStatistics(this.questionId);
      }
    });
  }

  loadQuestionStatistics(questionId: number) {
    this.questionClient.getQuestionStatistics(questionId).subscribe(statistics => {
      const canvas = document.getElementById('questionStatisticsChart') as HTMLCanvasElement;
      const ctx = canvas?.getContext('2d');

      if (ctx) {
        Chart.register(LinearScale, ArcElement, DoughnutController, Title, Tooltip, CategoryScale);

        new Chart(ctx, {
          type: 'doughnut',
          data: {
            labels: ['Percentage of Correct Answers', 'Percentage of Incorrect Answers'],
            datasets: [{
              label: 'Answer Statistics',
              data: [statistics.correctAnswers, statistics.wrongAnswers],
              backgroundColor: ['rgba(75, 192, 192, 0.2)', 'rgba(255, 99, 132, 0.2)'],
              borderColor: ['rgba(75, 192, 192, 1)', 'rgba(255, 99, 132, 1)'],
              borderWidth: 1
            }]
          },
          options: {
            scales: {
              y: { beginAtZero: true }
            }
          }
        });
      }
    });
  }
}
