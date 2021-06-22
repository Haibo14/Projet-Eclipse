using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject oscMaster;
    public ReceivePosition oscMessage;

    public GameObject pauseMenuUI;
    public GameObject basePauseMenu;
    public GameObject reprendre;
    public GameObject recommencer;
    public GameObject options;
    public GameObject menu;
    public GameObject options_Menu;
    public GameObject volume;
    public GameObject audioSlider;
    public GameObject music;
    public GameObject audioSlider2;
    public GameObject commands;
    public GameObject back;
    public GameObject commandsImage;

    Image reprendre_Image;
    Image recommencer_Image;
    Image options_Image;
    Image menu_Image;
    Image volume_Image;
    Image music_Image;
    Image commands_Image;
    Image back_Image;

    TextMesh reprendre_Text;
    TextMesh recommencer_Text;
    TextMesh options_Text;
    TextMesh menu_Text;
    TextMesh options_Menu_Text;
    TextMesh volume_Text;
    TextMesh music_Text;
    TextMesh commands_Text;
    TextMesh back_Text;

    Color reprendre_BaseColor;
    Color reprendre_ChosenColor;
    Color recommencer_BaseColor;
    Color recommencer_ChosenColor;
    Color options_BaseColor;
    Color options_ChosenColor;
    Color menu_BaseColor;
    Color menu_ChosenColor;
    Color volume_baseColor;
    Color volume_chosenColor;
    Color music_baseColor;
    Color music_chosenColor;
    Color commands_baseColor;
    Color commands_chosenColor;
    Color back_baseColor;
    Color back_chosenColor;

    public float transparency;
    public float timer;
    public float latency;
    public float sliderSensitivity;

    public int selectedStateY;
    public int selectedStateZ;

    Vector2 move;

    public string moveX;
    public string moveY;

    public string submitString;
    public string cancelString;

    bool jumpButton;
    bool interactButton;

    void Start()
    {
        oscMessage = oscMaster.GetComponent<ReceivePosition>();

        reprendre_Image = reprendre.GetComponent<Image>();
        recommencer_Image = recommencer.GetComponent<Image>();
        options_Image = options.GetComponent<Image>();
        menu_Image = menu.GetComponent<Image>();
        volume_Image = volume.GetComponent<Image>();
        music_Image = music.GetComponent<Image>();
        commands_Image = commands.GetComponent<Image>();
        back_Image = back.GetComponent<Image>();

        reprendre_Text = reprendre.transform.GetChild(0).GetComponent<TextMesh>();
        recommencer_Text = recommencer.transform.GetChild(0).GetComponent<TextMesh>();
        options_Text = options.transform.GetChild(0).GetComponent<TextMesh>();
        menu_Text = menu.transform.GetChild(0).GetComponent<TextMesh>();
        volume_Text = volume.transform.GetChild(0).GetComponent<TextMesh>();
        music_Text = music.transform.GetChild(0).GetComponent<TextMesh>();
        commands_Text = commands.transform.GetChild(0).GetComponent<TextMesh>();
        back_Text = back.transform.GetChild(0).GetComponent<TextMesh>();

        reprendre_BaseColor = reprendre_Image.color;
        reprendre_ChosenColor = reprendre_Image.color;
        reprendre_ChosenColor.a = transparency;

        recommencer_BaseColor = recommencer_Image.color;
        recommencer_ChosenColor = recommencer_Image.color;
        recommencer_ChosenColor.a = transparency;

        options_BaseColor = options_Image.color;
        options_ChosenColor = options_Image.color;
        options_ChosenColor.a = transparency;

        menu_BaseColor = menu_Image.color;
        menu_ChosenColor = menu_Image.color;
        menu_ChosenColor.a = transparency;

        volume_baseColor = volume_Image.color;
        volume_chosenColor = volume_Image.color;
        volume_chosenColor.a = transparency;

        music_baseColor = music_Image.color;
        music_chosenColor = music_Image.color;
        music_chosenColor.a = transparency;

        commands_baseColor = commands_Image.color;
        commands_chosenColor = commands_Image.color;
        commands_chosenColor.a = transparency;

        back_baseColor = back_Image.color;
        back_chosenColor = back_Image.color;
        back_chosenColor.a = transparency;


        jumpButton = false;
        interactButton = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(GameIsPaused == true)
        {
            move.x = Input.GetAxis(moveX);
            move.y = Input.GetAxis(moveY); 
            
            if (move == Vector2.zero)
            {
                move.x = -(oscMessage.xAxis_p1);
                move.y = oscMessage.zAxis_p1;
            }

            jumpButton = oscMaster.GetComponent<ReceivePosition>().buttonJumpP1;
            interactButton = oscMaster.GetComponent<ReceivePosition>().buttonInteractP2;

            timer += Time.unscaledDeltaTime;

            if (timer >= latency)
            {
                if (move.y > 0)
                {
                    selectedStateY--;

                    timer = 0;
                }
                else if (move.y < 0)
                {
                    selectedStateY++;

                    timer = 0;
                }

                if (Input.GetButton(cancelString))
                {
                    selectedStateZ--;

                    timer = 0;
                }
            }

            if (selectedStateZ == 0)
            {

                basePauseMenu.SetActive(true);
                options_Menu.SetActive(false);

                if (selectedStateY == 0)
                {
                    reprendre_Image.color = reprendre_ChosenColor;
                    options_Image.color = options_BaseColor;
                    menu_Image.color = menu_BaseColor;
                    recommencer_Image.color = recommencer_BaseColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString))
                        {
                            Resume();

                            timer = 0;
                        }
                    }
                }
                else if (selectedStateY == 1)
                {
                    reprendre_Image.color = reprendre_BaseColor;
                    recommencer_Image.color = recommencer_ChosenColor;
                    options_Image.color = options_BaseColor;
                    menu_Image.color = menu_BaseColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString))
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        }
                    }
                }
                else if (selectedStateY == 2)
                {
                    reprendre_Image.color = reprendre_BaseColor;
                    options_Image.color = options_ChosenColor;
                    menu_Image.color = menu_BaseColor;
                    recommencer_Image.color = recommencer_BaseColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString))
                        {
                            basePauseMenu.SetActive(false);
                            options_Menu.SetActive(true);

                            selectedStateZ++;
                            selectedStateY = 0;

                            timer = 0;
                        }
                    }
                }
                else if (selectedStateY == 3)
                {
                    reprendre_Image.color = reprendre_BaseColor;
                    options_Image.color = options_BaseColor;
                    menu_Image.color = menu_ChosenColor;
                    recommencer_Image.color = recommencer_BaseColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString))
                        {
                            SceneManager.LoadScene(0);

                            timer = 0;
                        }
                    }

                }
                else if (selectedStateY >= 4)
                {
                    selectedStateY = 0;
                }
                else if (selectedStateY <= -1)
                {
                    selectedStateY = 3;
                }
            }
            else if (selectedStateZ == 1)
            {
                if (selectedStateY == 0)
                {
                    volume_Image.color = volume_baseColor;
                    commands_Image.color = commands_baseColor;
                    back_Image.color = back_baseColor;
                    music_Image.color = music_chosenColor;

                    audioSlider2.GetComponent<Slider>().value += (move.x * sliderSensitivity * Time.unscaledDeltaTime);
                }
                else if (selectedStateY == 1)
                {
                    volume_Image.color = volume_chosenColor;
                    commands_Image.color = commands_baseColor;
                    back_Image.color = back_baseColor;
                    music_Image.color = music_baseColor;

                    audioSlider.GetComponent<Slider>().value += (move.x * sliderSensitivity * Time.unscaledDeltaTime);
                }
                else if (selectedStateY == 2)
                {
                    volume_Image.color = volume_baseColor;
                    commands_Image.color = commands_chosenColor;
                    back_Image.color = back_baseColor;
                    music_Image.color = music_baseColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString) || jumpButton == true)
                        {
                            commandsImage.SetActive(true);
                            options_Menu.SetActive(false);
                            back.SetActive(false);

                            timer = 0;
                        }
                        else
                        {
                            commandsImage.SetActive(false);
                            options_Menu.SetActive(true);
                            back.SetActive(true);
                        }
                    }
                }
                else if (selectedStateY == 3)
                {
                    volume_Image.color = volume_baseColor;
                    commands_Image.color = commands_baseColor;
                    back_Image.color = back_chosenColor;
                    music_Image.color = music_baseColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString))
                        {
                            selectedStateZ--;

                            timer = 0;
                        }
                    }
                }
                else if (selectedStateY >= 3)
                {
                    selectedStateY = 0;
                }
                else if (selectedStateY <= -1)
                {
                    selectedStateY = 3;
                }
            }
            else if(selectedStateZ <= -1)
            {
                Resume();
                selectedStateZ = 0;
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
