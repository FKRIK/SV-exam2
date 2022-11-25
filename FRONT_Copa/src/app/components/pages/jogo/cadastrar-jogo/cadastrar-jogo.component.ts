import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { Selecao } from "src/app/models/selecao.model";
import { Jogo } from "src/app/models/jogo.model";

@Component({
  selector: "app-cadastrar-jogo",
  templateUrl: "./cadastrar-jogo.component.html",
  styleUrls: ["./cadastrar-jogo.component.css"],
})
export class CadastrarJogoComponent implements OnInit {
  timeA!: Selecao;
  timeB!: Selecao;
  selecoes!: Selecao[];

  constructor(
    private http: HttpClient,
    private router: Router,
    private _snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.http
      .get<Selecao[]>("https://localhost:5001/api/selecao/listar")
      .subscribe({
        next: (funcionarios) => {
          this.selecoes = funcionarios;
        },
      });
  }

  cadastrar(): void {
    console.log(this.timeA);
    console.log(this.timeB);
    // Ao clicar cadastrar, pega o id das seleções

    let jogo: Jogo = {
      selecaoA: this.timeA,
      selecaoB: this.timeB,
    };
    console.log(jogo);

    this.http
      .post<Jogo>("https://localhost:5001/api/jogo/cadastrar", jogo)
      .subscribe({
        next: (funcionario) => {
          this._snackBar.open("Jogo cadastrado!", "Ok!", {
            horizontalPosition: "right",
            verticalPosition: "top",
          });
          this.router.navigate(["pages/jogo/listar"]);
        },
        error: (error) => {
          console.error("Algum erro aconteceu!");
        },
      });
  }
}
