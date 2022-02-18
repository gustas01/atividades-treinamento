#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

typedef enum {false, true} bool;

typedef struct no{
    int valor;
    struct no *prox;
}noCedula;

noCedula *pilha[7];
int valorNota[7] = {1, 2, 5, 10, 20, 50, 100};

void inicializaPilha (){
    int i;
    for (i = 0; i < 7; i++){
        pilha[i] = NULL;
        }
}


void abastecePilha (int quant, int v){
    int i;
    int pos;
    noCedula *novo;
    switch (v){
    case 1:
        pos = 0;
        break;
    case 2:
        pos = 1;
        break;
    case 5:
        pos = 2;
        break;
    case 10:
        pos = 3;
        break;
    case 20:
        pos = 4;
        break;
    case 50:
        pos = 5;
        break;
    case 100:
        pos = 6;
        break;
        default: printf("Valor de c�dula inv�lido\n");
        }
    for (i=0; i<quant; i++){
        novo = (noCedula*) malloc(sizeof(noCedula));
        novo->valor = v;
        novo->prox = pilha[pos];//n�o sei se � NULL mesmo
        pilha[pos] = novo;

        }
}


void imprimeSaldo (){
    noCedula *atual;
    int i, soma=0;
    //para cada pilha
        for(i=0; i<7; i++){
            atual = pilha[i];
    //percorre cada pilha
            while(atual!=NULL){
    //acumulando a soma
        soma += atual->valor;
    //proximo elemento
    atual = atual->prox;
            }//end while
        }//end for
    printf("saldo = %d\n", soma);
}//end imprimeSaldo


void imprimeSomaCedulas(){
    noCedula *atual;
    int i, soma=0;
    //para cada pilha
        for(i=0;i<7; i++){
            atual = pilha[i];
            soma = 0;
    //percorre cada pilha
                while(atual!=NULL){
    //acumulando a soma
                soma += atual->valor;
    //proximo elemento
                atual = atual->prox;
                }//end while
        printf("Saldo em notas de %d = %d\n", valorNota[i], soma);
    }//end for
    printf("\n");
}//end imprimeSomaCedulas



bool saqueDisponivel(int valor){
    int i;
    noCedula *atual;
    for(i=6; i >=0; i--){
        atual = pilha[i]; //inicio de pilha
        //enquanto tem elementos na pilha e
        //enquanto eh possivel dividir o valor pelo valor das cedulas
        while((atual!=NULL) && ((valor / valorNota[i]) >= 1)){
                //decrementa o valor
        valor -= valorNota[i];
        //proximo elemento da pilha
        atual = atual->prox;
        }//end while
    }//end for
    if(valor == 0){
        return true;
    }else{
        return false;
    }//end else
}//end saqueDisponivel



bool saque(int valor){
    int i;
    noCedula *atual;
    if(saqueDisponivel(valor)==true){
        for(i=6; i >=0; i--){
            //enquanto tem elementos na pilha e
            //enquanto eh poss�vel dividir o valor pelo valor das cedulas
            while((pilha[i] != NULL)&&((valor / valorNota[i]) >= 1)){
            //decrementa o valor
            valor -= valorNota[i];
            //pegando o elemento a ser removido da pilha
            atual = pilha[i];
            //mundando o novo primeiro da pilha
            pilha[i] = atual->prox;
            //apagando atual
            free(atual);
            }//end while
        }//end for
        return true;
    }else{
        printf("Quatidade de cedulas indisponivel para valor solicitado\n");
        return false;
    }//end else
}//end saque




int main (int argc, char *argv[]){
    setlocale (LC_ALL, "Portuguese");
    int quantidade, valor, i, valorSaque;
    for (i = 6; i >= 0; i--){
        printf("Digite a quantidade de notas de %d reais que deseja inserir na pilha\n", valorNota[i]);
        scanf("%d", &quantidade);
        valor = valorNota[i];
        abastecePilha(quantidade, valor);
        imprimeSaldo();
        imprimeSomaCedulas();
    }
    //system("cls");
    imprimeSaldo();

    printf("\nDigite o valor que deseja sacar: \n");
    scanf("%d", &valorSaque);

    if (saqueDisponivel(valorSaque) == true)
        printf("Valor dispon�vel\n");

    if (saque(valorSaque) == true){
        imprimeSaldo();
        printf("Valor sacado com sucesso!\n\n");
    }
        imprimeSomaCedulas();
}
