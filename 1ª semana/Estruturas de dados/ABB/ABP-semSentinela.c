#include "ABP.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

struct no{
    int chave;
    struct no *esq;
    struct no *dir;
    struct no *pai;
};

struct arvore{
    struct no *primeiroElemento;
    int length;
};


arvore* criaArvore(){
    arvore *A = (arvore*) malloc (sizeof(arvore));
    A->primeiroElemento = NULL;
    A->length = 0;

    return A;
}


void insereNo(arvore *A, int chave){

    if(verificaRepeticao(A, chave)){
        printf("VALOR REPETIDO\n");    
        return;
    } 


    
    A->length++;
    no *atual = A->primeiroElemento;
    no *ant = NULL;
    no *novo = (no*) malloc (sizeof(no));
    novo->dir = NULL;
    novo->esq = NULL;

    novo->chave = chave;

    while (atual != NULL){
        ant = atual;
            if (novo->chave < atual->chave)
                atual = atual->esq;
            else
                atual = atual->dir;
    }

        if (ant == NULL){
            printf("inserindo primeiro elemento\n\n");
            A->primeiroElemento = novo;
            novo->pai = NULL;
        }

        else {
            if (atual == NULL){
                if (novo->chave < ant->chave)
                    ant->esq = novo;
                
                else
                    ant->dir = novo;
            }

                   novo->pai = ant;
            
        }
}


void insereArquivo(arvore *A, char nomeArquivo[]){
    FILE *file = fopen(nomeArquivo, "r");
    int valor;

    while(!feof(file)){
        fscanf(file, "%d\n", &valor);
        insereNo(A, valor);
    }


  fclose(file);
}

//em ordem
void percorreArvoreEmOrdem(no *raiz){
    if (raiz != NULL){
        percorreArvoreEmOrdem(raiz->esq);
        printf("%d ", raiz->chave);
        percorreArvoreEmOrdem(raiz->dir);
    }
    
}

void percorreArvorePreOrdem(no *raiz){
    if (raiz != NULL){
        printf("%d ", raiz->chave);
        percorreArvoreEmOrdem(raiz->esq);
        percorreArvoreEmOrdem(raiz->dir);
    }
    
}

void percorreArvorePosOrdem(no *raiz){
    if (raiz != NULL){
        percorreArvoreEmOrdem(raiz->esq);
        percorreArvoreEmOrdem(raiz->dir);
        printf("%d ", raiz->chave);
    }
    
}

bool verificaRepeticao(arvore *A, int chave){
    no *valorBuscado = retornaNo(A, chave);

    if(valorBuscado != NULL)
        return true;
    else
        return false;
}

void apagaArvore(no *noArvore){
    if (noArvore != NULL){
        apagaArvore(noArvore->esq);
        free (noArvore);
        apagaArvore(noArvore->dir);
    }
    
}


void imprimeDados(no *elemento){

    printf("Chave:%d", elemento->chave);
    if(elemento->esq == NULL) printf("\nEsq:NULO");
    else printf("\nEsq:%d", elemento->esq->chave);
    if(elemento->dir == NULL) printf("\nDir:NULO");
    else printf("\nDir:%d", elemento->dir->chave);
    if(elemento->pai == NULL) printf("\nPai:NULO");
    else printf("\nPai:%d", elemento->pai->chave);        
}


no *retornaRaiz(arvore *A){
    return A->primeiroElemento;
}


no *retornaNo(arvore *A, int chave){
    no *atual = A->primeiroElemento;

    while (atual != NULL) {
        if (atual->chave == chave) return atual;

        if (atual->chave > chave) atual = atual->esq;
        else atual = atual->dir;
    }
}


void removeNo(arvore *A, int chave){ 
    no * pai;
    no *pt = A->primeiroElemento;
    no *ptaux;
    /* a busca */
    do{ 
        pai = pt ;
        if (chave < pt->chave) 
            pt = pt ->esq ;
        else if ( chave > pt-> chave) 
                pt = pt->dir;
            // else
            //     pai = pt->pai;
         
    } while((pt != NULL) && (pt->chave != chave));


    if (pt != NULL){ //encontrou o chave na ??rvore a ser removido

        A->length--;
        if ((pt->esq != NULL) && (pt->dir != NULL)){
            //aqui, ptaux passa a ser o elemento a ser removido
            ptaux = pt;
            pt = pt->dir;
            pai = pt->pai; 
            while (pt->esq != NULL) { // acha o imediatamente maior
                pai = pt;
                pt = pt->esq;
            }
            //no while acima, pt passa a ser o ??ltimo elemento mais a esquerda da chave a direita de ptaux
            // troca o chave do n?? a ser retirado pelo imediatamente maior
            //ptaux ?? a chave que quero remover da ??rvore
            ptaux->chave = pt->chave;
        }

        if ((pt->esq != NULL) && (pt->dir == NULL)){
            /* s?? tem o filho esq */
            if(pai == pt)
                A->primeiroElemento = pt->esq;
            
            if (pai->esq == pt){ 
                pai->esq = pt->esq;
                // pt->esq->pai = pai;
            }
            else {
                pai->dir = pt->esq;
                // pt->esq->pai = pai;
                }
         }else if ((pt->esq == NULL) && (pt->dir != NULL)){
                /* s?? tem o filho direito */
                if(pai == pt)
                    A->primeiroElemento = pt->dir;
                
                if (pai->esq == pt){
                    pai->esq = pt->dir;
                    pt->dir->pai = pai;
                }
                else {
                    pai->dir = pt->dir;
                    pt->dir->pai = pai;
                }
                } else{/* ent??o ?? folha */
                if (pai->esq == pt) 
                    pai->esq = NULL;
                    
                else 
                    pai->dir = NULL;
                    
                    
            }
         free (pt);
        }else{
            printf("Valor nao existente na arvore\n");
        }
}