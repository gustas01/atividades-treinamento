#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <locale.h>

typedef struct no{
    char nome[30], disciplina[20];
    int matricula;
    float coeficiente;
    struct no *prox;
} noAluno;


typedef struct noLista{
    int length;
    struct no *primeiroAluno;
} listaAlunos;


bool insereAlunoLista(noAluno *insere, listaAlunos *lista){
       noAluno *ant = NULL;
       noAluno *atual = lista->primeiroAluno;
       noAluno *novo = (noAluno*) malloc (sizeof(noAluno));
       while (atual != NULL){
           ant = atual;
           atual = atual -> prox;
       }

       novo = insere;
       novo ->prox = atual;

       if (ant == NULL){//ocorre quando a lista est� vazia
           lista->primeiroAluno = novo;
        }

       else{
            ant->prox = novo;
       }


       lista->length++;
       return true;
       }


void imprimeLista (listaAlunos *lista){
    noAluno *atual2 = lista->primeiroAluno;
    while (atual2 != NULL){
        printf ("Nome: %s\nMatricula: %d\nDisciplina: %s\nCoeficiente: %.2f\n\n", atual2->nome, atual2->matricula, atual2->disciplina, atual2->coeficiente);
        atual2 = atual2->prox;
    }
}

void apagaLista (listaAlunos *lista){
    noAluno *atual = lista->primeiroAluno;
    noAluno *atual2;
    while (atual != NULL){
        atual2 = atual;
        atual = atual->prox;
        free(atual2);
    }
}

bool removeAluno (int mat, listaAlunos *lista){
    noAluno *atual = lista->primeiroAluno;
    noAluno *ant = NULL;

    while ((atual != NULL) && (atual ->matricula != mat)){
        ant = atual;
        atual = atual->prox;
    }
    if (atual == NULL){
        printf("\n\nO ALUNO BUSCADO NAO CONSTA NA LISTA\n");
        return false;
    }

    if (ant == NULL){ //removendo o primeiro aluno da lista
        lista->primeiroAluno = atual->prox;
    }


    else
        ant->prox = atual->prox;
        
        lista->length--;
        free(atual);
}

bool buscaAluno (int buscaMat, listaAlunos *lista){
    noAluno *atual = lista->primeiroAluno;
    noAluno *ant = NULL;
    while ((atual != NULL) && (buscaMat != atual ->matricula)){
        ant = atual;
        atual = atual->prox;
    }
    if (atual == NULL){
        printf("Aluno nao encontrado\n\n");
        return false;
        }
    else{
        printf("Nome: %s\nMatricula: %d\nDisciplina: %s\nCoeficiente: %.2f\n\n", atual->nome, atual->matricula, atual->disciplina, atual->coeficiente);
    return true;
    }

}


int main(int argc, char *argv[]){
    setlocale(LC_ALL, "Portuguese");//habilita a acentua��o nos printfs
    listaAlunos *lista = (listaAlunos*)malloc (sizeof(listaAlunos));

    lista->length = 0;
    int a = 1;
    printf("O tamanho da lista e %d\n\n", lista->length);
    
    printf ("Digite os dados do aluno\n");
    while (a != 0){
        noAluno *aux = (noAluno*) malloc (sizeof(noAluno));
        printf ("Nome: ");
        scanf ("%s", aux->nome);
        printf ("Matricula: ");
        scanf ("%d", &aux->matricula);
        printf ("Disciplina: ");
        scanf ("%s", aux->disciplina);
        printf ("Coeficiente: ");
        scanf ("%f", &aux->coeficiente);

        insereAlunoLista(aux, lista);
        printf("\nO tamanho da lista e: %d\n\n", lista->length);


        printf("Digite 0 para encerrar a insercao de aluno\n       1 para inserir outro aluno: ");
        scanf ("%d", &a);

    }
    system("cls");
    printf("o primeiro elemento da lista e: %s\n\n", lista->primeiroAluno->nome);
    // system("pause");
    imprimeLista(lista);

int z, matri;

printf("\n\nDigite o numero de matricula que deseja remover: ");
scanf("%d", &z);


system("cls");
removeAluno(z, lista);
printf("O primeiro elemento da lista e %s:\n\n", lista->primeiroAluno->nome);

imprimeLista(lista);

printf("\n\nDigite um numero de matricula a ser buscado: ");
scanf("%d", &matri);
system("cls");
buscaAluno(matri, lista);

}
