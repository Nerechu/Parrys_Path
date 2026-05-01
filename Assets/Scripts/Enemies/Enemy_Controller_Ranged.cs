using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller_Ranged : MonoBehaviour, IEnemyController
{
    // ============================
    //      ESTADOS DEL ENEMIGO
    // ============================
    public enum EstadoEnemigo
    {
        Idle = 0,
        Persiguiendo = 1,
        Telegraph = 2,
        Atacando = 3,
        Cooldown = 4,
        Hurt = 5,
        Muerto = 6
    }

    [Header("Estado actual")]
    public EstadoEnemigo estadoActual = EstadoEnemigo.Idle;


    // ============================
    //      MÓDULOS DEL ENEMIGO
    // ============================
    [Header("Módulos del enemigo")]
    public Enemy_Seguimiento seguimiento;
    public Enemy_Attack_Ranged rangedAttackModule;
    public Enemy_Health healthModule;


    // ============================
    //      COMPONENTES INTERNOS
    // ============================
    private Animator animator;
    private Rigidbody2D rb;
    private Transform player;


    // ============================
    //      INICIALIZACIÓN
    // ============================
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ⭐ Inicializar seguimiento
        if (seguimiento != null)
        {
            seguimiento.objetivo = player;
            seguimiento.Initialize(this, animator, rb, transform);
        }

        // ⭐ Inicializar ataque a distancia
        if (rangedAttackModule != null)
            rangedAttackModule.Initialize(this, animator, player);

        // ⭐ Inicializar vida (tu script no necesita Initialize)
        if (healthModule != null)
        {
            // Enemy_Health no requiere inicialización
        }
    }


    // ============================
    //      UPDATE GENERAL
    // ============================
    void Update()
    {
        if (estadoActual == EstadoEnemigo.Muerto)
            return;

        // ⭐ Lógica de seguimiento
        seguimiento?.ActualizarLogica();

        // ⭐ Lógica de ataque a distancia
        rangedAttackModule?.ActualizarAtaque();
    }


    // ============================
    //      MOVIMIENTO FÍSICO
    // ============================
    void FixedUpdate()
    {
        seguimiento?.ActualizarMovimientoFisico();
    }


    // ============================
    //      INTERFAZ IEnemyController
    // ============================
    public void CambiarEstado(int nuevoEstado)
    {
        estadoActual = (EstadoEnemigo)nuevoEstado;
    }

    public int GetEstadoActual()
    {
        return (int)estadoActual;
    }
}
