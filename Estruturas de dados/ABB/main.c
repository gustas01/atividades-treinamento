#include <stdio.h>
#include <stdlib.h>
#include "ABP.h"
#include "ABP.c"

void main(){
    int valor = 0;

    arvore *A = criaArvore(); //tá de boa
    // insereArquivo(A, "valores.txt"); //tá de boa


    while (valor != -1){
        printf("O tamanho da arvore e: %d\n", A->length);
        printf("Digite o valor para ser inserido na arvore, ou -1 para terminar a insercao:\n");
        scanf("%d", &valor);
        if (valor != -1) 
            insereNo(A, valor);
    }

    system("cls");

    printf("O tamanho da arvore e: %d\n", A->length);
    percorreArvoreEmOrdem(A->sentinela->dir); //tá de boa
    printf("\n");
    // imprimeDados(A->sentinela->dir->esq->esq); //tá de boa

    printf("Digite um valor para ser removido da arvore: ");
    scanf("%d", &valor);
    removeNo(A, valor);
    
    printf("O tamanho da arvore e: %d\n", A->length);
    percorreArvoreEmOrdem(A->sentinela->dir); //tá de boa

}
    