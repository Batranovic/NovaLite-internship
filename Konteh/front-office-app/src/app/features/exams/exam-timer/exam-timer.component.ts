import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GetExamResponse } from '../../../api/api-reference';

@Component({
  selector: 'app-exam-timer',
  templateUrl: './exam-timer.component.html',
  styleUrls: ['./exam-timer.component.css']
})
export class ExamTimerComponent implements OnInit {
  @Output() timerExpired = new EventEmitter<void>()
  @Input() exam! : GetExamResponse;
  totalTime: number = 240 //TO DO: change the value of totalTime 
  timer: any
  formattedTime: string = '';
  
  ngOnInit() {
    this.initializeTimer()
  }

  initializeTimer() {
    const currentDate =new Date()
    const currentTime = currentDate.getTime()
    const startTime = this.exam.startTime!.getTime(); 
    const endTime = startTime + this.totalTime * 1000; 

    if (currentTime <= endTime) {
      const remainingTime = Math.floor((endTime - currentTime) / 1000); 
      this.totalTime = Math.max(remainingTime, 0);
    } else {
      this.totalTime = 0;
    }

    setTimeout(() => {
      this.startTimer()
    }, 1000)
  }

  startTimer() {
    if (this.totalTime <= 0) {
      this.formattedTime = '0 seconds'; 
      this.timerExpired.emit(); 
      return;
    }
    
    this.timer = setInterval(() => {
      this.totalTime--
      this.updateDisplayTime();
      if (this.totalTime === 0) {
        clearInterval(this.timer)
        this.timerExpired.emit()
      }
    }, 1000)
  }

  updateDisplayTime(): void {
    const minutes = Math.floor(this.totalTime / 60);
    const seconds = this.totalTime % 60;
    
    if (this.totalTime >= 60) {
      this.formattedTime = `${minutes} minute${minutes !== 1 ? 's' : ''} ${seconds} second${seconds !== 1 ? 's' : ''}`;
    } else { 
      this.formattedTime = `${seconds} second${seconds !== 1 ? 's' : ''}`;
    }
  }

}
