#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <locale.h>
#define NUM_FILAS 3
#define TAM_NOME 50

typedef enum {false, true} bool;

typedef struct no{
    float tamanho;
    char nome[TAM_NOME];
    int prioridade;
    struct no *prox;
}noProcesso;

noProcesso *filaDeProcessos[NUM_FILAS];

bool prioridadeProcessos[10];

noProcesso *ult0 = NULL;
noProcesso *ult1 = NULL;
noProcesso *ult2 = NULL;

int quant = 0;


void inicializaFila (){
    int i;
    for (i = 0; i < NUM_FILAS; i++){
        filaDeProcessos[i] = NULL;
    }
}


void inicializaVetorPrioridades (){
    int i;
    for (i = 0; i < 10; i++)
        prioridadeProcessos[i] = false;
}



void insereFila (float tamanhoProcesso, char nomeProcesso[], int prioridade){
    int i;
    noProcesso *novo = (noProcesso*) malloc(sizeof(noProcesso));

    if ((prioridade >= 0) && (prioridade <= 3)){

            strcpy(novo->nome, nomeProcesso);
            novo->tamanho = tamanhoProcesso;
            novo->prioridade = prioridade;
            novo->prox = NULL;

            if (filaDeProcessos[0] == NULL)
                filaDeProcessos[0] = novo;

            if (ult0 != NULL)
                ult0->prox = novo;

            ult0 = novo;
            quant++;

           // filaDeProcessos[0]->prox = ult0;
    }

    if ((prioridade >= 4) && (prioridade <= 6)){

            strcpy(novo->nome, nomeProcesso);
            novo->tamanho = tamanhoProcesso;
            novo->prioridade = prioridade;
            novo->prox = NULL;

            if (filaDeProcessos[1] == NULL)
                filaDeProcessos[1] = novo;

            if (ult1 != NULL)
                ult1->prox = novo;

            ult1 = novo;
            quant++;

           // filaDeProcessos[0]->prox = ult0;
    }

    if ((prioridade >= 7) && (prioridade <= 9)){

            strcpy(novo->nome, nomeProcesso);
            novo->tamanho = tamanhoProcesso;
            novo->prioridade = prioridade;
            novo->prox = NULL;

            if (filaDeProcessos[2] == NULL)
                filaDeProcessos[2] = novo;

            if (ult2 != NULL)
                ult2->prox = novo;

            ult2 = novo;
            quant++;

           // filaDeProcessos[0]->prox = ult0;
    }
}




void imprimeDadosListaDeProcessos (){
noProcesso *atual;
int i;
    for (i = 0; i < NUM_FILAS; i++){
        atual = filaDeProcessos[i];
        if (i == 0)
        printf("[INDICE 0] Processos com prioridade entre 0 e 3\n");
        if (i == 1)
        printf("\n[INDICE 1] Processos com prioridade entre 4 e 6\n");
        if (i == 2)
        printf("\n[INDICE 2] Processos com prioridade entre 7 e 9\n\n");
            while (atual != NULL){
            printf("Nome: %sTamanho: %.2f\nPrioridade: %d\n\n", atual->nome, atual->tamanho, atual->prioridade);
            atual = atual->prox;
            }
    }
}


bool removePrimeiroDaFila (int indice){
    if (filaDeProcessos[indice] == NULL) return false;

    noProcesso *desaloca = filaDeProcessos[indice];
    desaloca = filaDeProcessos[indice];
    filaDeProcessos[indice] = filaDeProcessos[indice]->prox;
    free(desaloca);

    return true;
}


int main (int argc, char *argv[]){
    setlocale(LC_ALL, "Portuguese");
    int a = 1, indice, indremove = 1;
    do {
        noProcesso *aux = (noProcesso*) malloc(sizeof(noProcesso));
        printf("Digite os dados do processo\n");

        printf("Nome: ");
        fflush(stdin);
        fgets(aux->nome, TAM_NOME, stdin);

        printf("Tamanho: ");
        scanf("%f", &aux->tamanho);

        printf("Prioridade: ");
        scanf("%d", &aux->prioridade);

        insereFila(aux->tamanho, aux->nome, aux->prioridade);
        system("cls");
        imprimeDadosListaDeProcessos();
        printf("Digite 0 para encerrar a insercao de processos\n       1 para inserir outro processo: ");
        scanf("%d", &a);
        system ("cls");
        imprimeDadosListaDeProcessos();
    }
    while (a != 0);

    system("cls");
    imprimeDadosListaDeProcessos();

    do {
        printf("Digite o indice da fila de onde deseja remover um elemento: ");
        scanf("%d", &indice);
    if (removePrimeiroDaFila(indice) == true){
        system("cls");
        printf("Processo removido com sucesso!\n");
        imprimeDadosListaDeProcessos();
        } else{
            printf("Processo nao existente!\n\n");
        }
        printf("Digite 0 para encerrar a remocao de processos\n       1 para remover outro processo: ");
        scanf("%d", &indremove);
        system("cls");
        imprimeDadosListaDeProcessos();
    }
    while (indremove != 0);
}
