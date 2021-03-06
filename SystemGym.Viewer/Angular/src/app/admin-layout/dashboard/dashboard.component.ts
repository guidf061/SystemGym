import { Component, OnInit } from '@angular/core';
import { LoaderService } from '../../core';
import { DashboardService } from '../../services/dashboard.service';
import { MatSnackBar } from '@angular/material';
import { Dashboard } from '../../models/dashboard-model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  dashboard: Dashboard = new Dashboard();

  getQuantidadeMatriculasMeses: Dashboard = new Dashboard();

  getQuantidadeMatriculasCanceladasMeses: Dashboard = new Dashboard();

  getRendimentos: Dashboard = new Dashboard();

  getInadiplentes: Dashboard = new Dashboard();

  lineBigDashboardChartType;
  gradientStroke;
  chartColor;
  canvas : any;
  ctx;
  gradientFill;
  lineBigDashboardChartData:Array<any>;
  lineBigDashboardChartOptions:any;
  lineBigDashboardChartLabels:Array<any>;
  lineBigDashboardChartColors:Array<any>

  gradientChartOptionsConfiguration: any;
  gradientChartOptionsConfigurationWithNumbersAndGrid: any;

  lineChartType;
  lineChartData:Array<any>;
  lineChartOptions:any;
  lineChartLabels:Array<any>;
  lineChartColors:Array<any>

  lineChartWithNumbersAndGridType;
  lineChartWithNumbersAndGridData:Array<any>;
  lineChartWithNumbersAndGridOptions:any;
  lineChartWithNumbersAndGridLabels:Array<any>;
  lineChartWithNumbersAndGridColors:Array<any>

  lineChartGradientsNumbersType;
  lineChartGradientsNumbersData:Array<any>;
  lineChartGradientsNumbersOptions:any;
  lineChartGradientsNumbersLabels:Array<any>;
  lineChartGradientsNumbersColors: Array<any>

  lineChartGradientsNumbersType1;
  lineChartGradientsNumbersData1: Array<any>;
  lineChartGradientsNumbersOptions1: any;
  lineChartGradientsNumbersLabel1: Array<any>;
  lineChartGradientsNumbersColors1: Array<any>
  // events
  chartClicked(e:any):void {
    console.log(e);
  }

  chartHovered(e:any):void {
    console.log(e);
  }
  hexToRGB(hex, alpha) {
    var r = parseInt(hex.slice(1, 3), 16),
      g = parseInt(hex.slice(3, 5), 16),
      b = parseInt(hex.slice(5, 7), 16);

    if (alpha) {
      return "rgba(" + r + ", " + g + ", " + b + ", " + alpha + ")";
    } else {
      return "rgb(" + r + ", " + g + ", " + b + ")";
    }
  }

  constructor(private loaderService: LoaderService,
    private dashboardService: DashboardService,
    private snackBar: MatSnackBar) {
    this.dashboard,
    this.getQuantidadeMatriculasMeses
  }

  ngOnInit() {

    this.loaderService.show();

    this.bigDashBoard();

    this.lineChartExample();

    this.lineChartExampleWithNumbersAndGrid();

    this.barChartSimpleGradientsNumbers();

    this.dashboardService.get().then(rows => {
      this.dashboard = rows;
      this.loaderService.hide();
      this.bigDashBoard();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.dashboardService.getQuantidadeMatriculasMes().then(rows => {
      this.getQuantidadeMatriculasMeses = rows;
      this.loaderService.hide();
      this.lineChartExample();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.dashboardService.getQuantidadeMatriculasCanceladas().then(rows => {
      this.getQuantidadeMatriculasCanceladasMeses = rows;
      this.loaderService.hide();
      this.lineChartExampleWithNumbersAndGrid();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.dashboardService.getRendimento().then(rows => {
      this.getRendimentos = rows;
      this.loaderService.hide();
      this.barChartSimpleGradientsNumbers();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.dashboardService.getInadiplentes().then(rows => {
      this.getInadiplentes = rows;
      this.loaderService.hide();
      this.barChartSimpleGradientsNumbers();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

  }

  bigDashBoard() {

    this.chartColor = "#FFFFFF";
    this.canvas = document.getElementById("bigDashboardChart");
    this.ctx = this.canvas.getContext("2d");

    this.gradientStroke = this.ctx.createLinearGradient(500, 0, 100, 0);
    this.gradientStroke.addColorStop(0, '#80b6f4');
    this.gradientStroke.addColorStop(1, this.chartColor);

    this.gradientFill = this.ctx.createLinearGradient(0, 200, 0, 50);
    this.gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    this.gradientFill.addColorStop(1, "rgba(255, 255, 255, 0.24)");

    if (this.dashboard === undefined || this.dashboard === null) {
      this.dashboard.janeiro = 0;
      this.dashboard.fevereiro = 0;
      this.dashboard.marco = 0;
      this.dashboard.abril = 0;
      this.dashboard.maio = 0;
      this.dashboard.junho = 0;
      this.dashboard.julho = 0;
      this.dashboard.agosto = 0;
      this.dashboard.setembro = 0;
      this.dashboard.outubro = 0;
      this.dashboard.novembro = 0;
      this.dashboard.dezembro = 0;
    }

    this.lineBigDashboardChartData = [
      {
        label: "Matr??cula",

        pointBorderWidth: 1,
        pointHoverRadius: 7,
        pointHoverBorderWidth: 2,
        pointRadius: 5,
        fill: true,

        borderWidth: 2,
        //jan,fev,mar, abr, mai,jun, jul, ago, set, out, nov, dez
        data:[
          this.dashboard.janeiro,
          this.dashboard.fevereiro,
          this.dashboard.marco,
          this.dashboard.abril,
          this.dashboard.maio,
          this.dashboard.junho,
          this.dashboard.julho,
          this.dashboard.agosto,
          this.dashboard.setembro,
          this.dashboard.outubro,
          this.dashboard.novembro,
          this.dashboard.dezembro
        ]
      }
    ];
    this.lineBigDashboardChartColors = [
      {
        backgroundColor: this.gradientFill,
        borderColor: this.chartColor,
        pointBorderColor: this.chartColor,
        pointBackgroundColor: "#2c2c2c",
        pointHoverBackgroundColor: "#2c2c2c",
        pointHoverBorderColor: this.chartColor,
      }
    ];
    this.lineBigDashboardChartLabels = ["JAN", "FEV", "MAR", "ABR", "MAI", "JUN", "JUL", "AGO", "SET", "OUT", "NOV", "DEZ"];
    this.lineBigDashboardChartOptions = {

      layout: {
        padding: {
          left: 20,
          right: 20,
          top: 0,
          bottom: 0
        }
      },
      maintainAspectRatio: false,
      tooltips: {
        backgroundColor: '#fff',
        titleFontColor: '#333',
        bodyFontColor: '#666',
        bodySpacing: 4,
        xPadding: 12,
        mode: "nearest",
        intersect: 0,
        position: "nearest"
      },
      legend: {
        position: "bottom",
        fillStyle: "#FFF",
        display: false
      },
      scales: {
        yAxes: [{
          ticks: {
            fontColor: "rgba(255,255,255,0.4)",
            fontStyle: "bold",
            beginAtZero: true,
            maxTicksLimit: 5,
            padding: 10
          },
          gridLines: {
            drawTicks: true,
            drawBorder: false,
            display: true,
            color: "rgba(255,255,255,0.1)",
            zeroLineColor: "transparent"
          }

        }],
        xAxes: [{
          gridLines: {
            zeroLineColor: "transparent",
            display: false,

          },
          ticks: {
            padding: 10,
            fontColor: "rgba(255,255,255,0.4)",
            fontStyle: "bold"
          }
        }]
      }
    };

    this.lineBigDashboardChartType = 'line';

  }

  lineChartExample() {
    this.canvas = document.getElementById("lineChartExample");
    this.ctx = this.canvas.getContext("2d");

    this.gradientStroke = this.ctx.createLinearGradient(500, 0, 100, 0);
    this.gradientStroke.addColorStop(0, '#80b6f4');
    this.gradientStroke.addColorStop(1, this.chartColor);

    this.gradientFill = this.ctx.createLinearGradient(0, 170, 0, 50);
    this.gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    this.gradientFill.addColorStop(1, "rgba(249, 99, 59, 0.40)");

    if (this.dashboard === undefined || this.dashboard === null) {
      this.getQuantidadeMatriculasMeses.janeiro = 0;
      this.getQuantidadeMatriculasMeses.fevereiro = 0;
      this.getQuantidadeMatriculasMeses.marco = 0;
      this.getQuantidadeMatriculasMeses.abril = 0;
      this.getQuantidadeMatriculasMeses.maio = 0;
      this.getQuantidadeMatriculasMeses.junho = 0;
      this.getQuantidadeMatriculasMeses.julho = 0;
      this.getQuantidadeMatriculasMeses.agosto = 0;
      this.getQuantidadeMatriculasMeses.setembro = 0;
      this.getQuantidadeMatriculasMeses.outubro = 0;
      this.getQuantidadeMatriculasMeses.novembro = 0;
      this.getQuantidadeMatriculasMeses.dezembro = 0;
    }

    this.lineChartData = [
      {
        label: "Matriculas Feitas",
        pointBorderWidth: 2,
        pointHoverRadius: 4,
        pointHoverBorderWidth: 1,
        pointRadius: 4,
        fill: true,
        borderWidth: 2,
        data: [
          this.getQuantidadeMatriculasMeses.janeiro,
          this.getQuantidadeMatriculasMeses.fevereiro,
          this.getQuantidadeMatriculasMeses.marco,
          this.getQuantidadeMatriculasMeses.abril,
          this.getQuantidadeMatriculasMeses.maio,
          this.getQuantidadeMatriculasMeses.junho,
          this.getQuantidadeMatriculasMeses.julho,
          this.getQuantidadeMatriculasMeses.agosto,
          this.getQuantidadeMatriculasMeses.setembro,
          this.getQuantidadeMatriculasMeses.outubro,
          this.getQuantidadeMatriculasMeses.novembro,
          this.getQuantidadeMatriculasMeses.dezembro
        ]
      }
    ];
    this.lineChartColors = [
      {
        borderColor: "#f96332",
        pointBorderColor: "#FFF",
        pointBackgroundColor: "#f96332",
        backgroundColor: this.gradientFill
      }
    ];
    this.lineChartLabels = ["Jan", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
    this.lineChartOptions = this.gradientChartOptionsConfiguration;

    this.lineChartType = 'line';
  }

  lineChartExampleWithNumbersAndGrid() {
    this.canvas = document.getElementById("lineChartExampleWithNumbersAndGrid");
    this.ctx = this.canvas.getContext("2d");

    this.gradientStroke = this.ctx.createLinearGradient(500, 0, 100, 0);
    this.gradientStroke.addColorStop(0, '#18ce0f');
    this.gradientStroke.addColorStop(1, this.chartColor);

    this.gradientFill = this.ctx.createLinearGradient(0, 170, 0, 50);
    this.gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    this.gradientFill.addColorStop(1, this.hexToRGB('#18ce0f', 0.4));

    if (this.dashboard === undefined || this.dashboard === null) {
      this.getQuantidadeMatriculasCanceladasMeses.janeiro = 0;
      this.getQuantidadeMatriculasCanceladasMeses.fevereiro = 0;
      this.getQuantidadeMatriculasCanceladasMeses.marco = 0;
      this.getQuantidadeMatriculasCanceladasMeses.abril = 0;
      this.getQuantidadeMatriculasCanceladasMeses.maio = 0;
      this.getQuantidadeMatriculasCanceladasMeses.junho = 0;
      this.getQuantidadeMatriculasCanceladasMeses.julho = 0;
      this.getQuantidadeMatriculasCanceladasMeses.agosto = 0;
      this.getQuantidadeMatriculasCanceladasMeses.setembro = 0;
      this.getQuantidadeMatriculasCanceladasMeses.outubro = 0;
      this.getQuantidadeMatriculasCanceladasMeses.novembro = 0;
      this.getQuantidadeMatriculasCanceladasMeses.dezembro = 0;
    }

    this.lineChartWithNumbersAndGridData = [
      {
        label: "Matriculas Cancelada",
        pointBorderWidth: 2,
        pointHoverRadius: 4,
        pointHoverBorderWidth: 1,
        pointRadius: 4,
        fill: true,
        borderWidth: 2,
        data: [
          this.getQuantidadeMatriculasCanceladasMeses.janeiro,
          this.getQuantidadeMatriculasCanceladasMeses.fevereiro,
          this.getQuantidadeMatriculasCanceladasMeses.marco,
          this.getQuantidadeMatriculasCanceladasMeses.abril,
          this.getQuantidadeMatriculasCanceladasMeses.maio,
          this.getQuantidadeMatriculasCanceladasMeses.junho,
          this.getQuantidadeMatriculasCanceladasMeses.julho,
          this.getQuantidadeMatriculasCanceladasMeses.agosto,
          this.getQuantidadeMatriculasCanceladasMeses.setembro,
          this.getQuantidadeMatriculasCanceladasMeses.outubro,
          this.getQuantidadeMatriculasCanceladasMeses.novembro,
          this.getQuantidadeMatriculasCanceladasMeses.dezembro
        ]
      }
    ];
    this.lineChartWithNumbersAndGridColors = [
      {
        borderColor: "#18ce0f",
        pointBorderColor: "#FFF",
        pointBackgroundColor: "#18ce0f",
        backgroundColor: this.gradientFill
      }
    ];
    this.lineChartWithNumbersAndGridLabels = ["Jan", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
    this.lineChartWithNumbersAndGridOptions = this.gradientChartOptionsConfigurationWithNumbersAndGrid;

    this.lineChartWithNumbersAndGridType = 'line';



  }

  barChartSimpleGradientsNumbers() {
    this.canvas = document.getElementById("barChartSimpleGradientsNumbers");
    this.ctx = this.canvas.getContext("2d");

    this.gradientFill = this.ctx.createLinearGradient(0, 170, 0, 50);
    this.gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
    this.gradientFill.addColorStop(1, this.hexToRGB('#2CA8FF', 0.6));

    if (this.dashboard === undefined || this.dashboard === null) {
      this.getRendimentos.janeiro = 0;
      this.getRendimentos.fevereiro = 0;
      this.getRendimentos.marco = 0;
      this.getRendimentos.abril = 0;
      this.getRendimentos.maio = 0;
      this.getRendimentos.junho = 0;
      this.getRendimentos.julho = 0;
      this.getRendimentos.agosto = 0;
      this.getRendimentos.setembro = 0;
      this.getRendimentos.outubro = 0;
      this.getRendimentos.novembro = 0;
      this.getRendimentos.dezembro = 0;
    }

    this.lineChartGradientsNumbersData = [
      {
        label: "Active Countries",
        pointBorderWidth: 2,
        pointHoverRadius: 4,
        pointHoverBorderWidth: 1,
        pointRadius: 4,
        fill: true,
        borderWidth: 1,
        data: [
          this.getRendimentos.janeiro,
          this.getRendimentos.fevereiro,
          this.getRendimentos.marco,
          this.getRendimentos.abril,
          this.getRendimentos.maio,
          this.getRendimentos.junho,
          this.getRendimentos.julho,
          this.getRendimentos.agosto,
          this.getRendimentos.setembro,
          this.getRendimentos.outubro,
          this.getRendimentos.novembro,
          this.getRendimentos.dezembro
        ]
      }
    ];
    this.lineChartGradientsNumbersColors = [
      {
        backgroundColor: this.gradientFill,
        borderColor: "#2CA8FF",
        pointBorderColor: "#FFF",
        pointBackgroundColor: "#2CA8FF",
      }
    ];
    this.lineChartGradientsNumbersLabels = ["Janeiro", "Fevereiro", "Marco", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
    this.lineChartGradientsNumbersOptions = {
      maintainAspectRatio: false,
      legend: {
        display: false
      },
      tooltips: {
        bodySpacing: 4,
        mode: "nearest",
        intersect: 0,
        position: "nearest",
        xPadding: 10,
        yPadding: 10,
        caretPadding: 10
      },
      responsive: 1,
      scales: {
        yAxes: [{
          gridLines: {
            zeroLineColor: "transparent",
            drawBorder: false
          }
        }],
        xAxes: [{
          display: 0,
          ticks: {
            display: false
          },
          gridLines: {
            zeroLineColor: "transparent",
            drawTicks: false,
            display: false,
            drawBorder: false
          }
        }]
      },
      layout: {
        padding: {
          left: 0,
          right: 0,
          top: 15,
          bottom: 15
        }
      }
    }

    this.lineChartGradientsNumbersType = 'bar';
  }
}
