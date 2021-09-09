import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'shortDate'
})
export class ShortDatePipe implements PipeTransform {
  //ng g p short-number --skip-tests. use = {{dog.views | shortNumber}}
  transform(value: any, args?: any): any {
    if (value) {
        const seconds = Math.floor((+new Date() - +new Date(value)) / 1000);
        if (seconds < 59) // less than 30 seconds ago will show as 'Just now'
            return 'vừa mới đăng';
        const intervals = {
            'năm': 31536000,
            'tháng': 2592000,
            'tuần': 604800,
            'ngày': 86400,
            'giờ': 3600,
            'phút': 60,
            'giây': 1
        };
        let counter;
        for (const i in intervals) {
            counter = Math.floor(seconds / intervals[i]);
            if (counter > 0){
              return counter + ' ' + i + ' trước'; // singular (1 day ago)
            }
        }
    }
    return value;
}

}
