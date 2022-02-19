#include <stdio.h>
#include <stdlib.h>
#include "ABP.h"
#include "ABP-semSentinela.c"

void main(){
    int valor = 0;

    arvore *A = criaArvore(); //tá de boa


    // while (valor != -1){
    //     printf("O tamanho da arvore e: %d\n", A->length);
    //     printf("Digite o valor para ser inserido na arvore, ou -1 para terminar a insercao:\n");
    //     scanf("%d", &valor);
    //     if (valor != -1) 
    //         insereNo(A, valor);
    // }


    insereArquivo(A, "aleatorios.txt");

    system("cls");

    printf("O tamanho da arvore e: %d\n", A->length);
    percorreArvoreEmOrdem(A->primeiroElemento); //tá de boa
    printf("\n");

    printf("Digite um valor para ser removido da arvore: ");
    scanf("%d", &valor);
    removeNo(A, valor);
    
    printf("\n\nO tamanho da arvore e: %d\n", A->length);
    percorreArvoreEmOrdem(A->primeiroElemento); //tá de boa

    printf("\nDigite o valor a ser buscado: ");
    scanf("%d", &valor);
    no *valorBuscado = retornaNo(A, valor);

    if(valorBuscado != NULL)
        printf("Imprimindo o valor retornado: %d\n", valorBuscado->chave);
    else
        printf("Valor buscado nao encontrado\n");

}
    