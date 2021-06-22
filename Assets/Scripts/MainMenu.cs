using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject oscMaster;
    public ReceivePosition oscMessage;

    public GameObject startMenu;
    public GameObject play;
    public GameObject eclypse;
    public GameObject options;
    public GameObject exit;
    public GameObject back;
    public GameObject eclypse_Menu;
    public GameObject options_Menu;
    public GameObject volume;
    public GameObject music;
    public GameObject audioSlider;
    public GameObject audioSlider2;
    public GameObject commands;
    public GameObject commandsImage;
    public GameObject levels_Menu;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject levelFinal;

    Image play_Image;
    Image eclypse_Image;
    Image options_Image;
    Image exit_Image;
    Image back_Image;
    Image volume_Image;
    Image music_Image;
    Image commands_Image;
    Image level1_Image;
    Image level2_Image;
    Image level3_Image;
    Image level4_Image;
    Image levelFinal_Image;

    TextMesh play_Text;
    TextMesh eclypse_Text;
    TextMesh options_Text;
    TextMesh exit_Text;
    TextMesh back_Text;
    TextMesh options_Menu_Text;
    TextMesh volume_Text;
    TextMesh music_Text;
    TextMesh commands_Text;

    Color play_baseColor;
    Color play_chosenColor;
    Color eclypse_baseColor;
    Color eclypse_chosenColor;
    Color options_baseColor;
    Color options_chosenColor;
    Color exit_baseColor;
    Color exit_chosenColor;
    Color back_baseColor;
    Color back_chosenColor;
    Color volume_baseColor;
    Color volume_chosenColor;
    Color music_baseColor;
    Color music_chosenColor;
    Color commands_baseColor;
    Color commands_chosenColor;
    Color level1_baseColor;
    Color level2_baseColor;
    Color level3_baseColor;
    Color level4_baseColor;
    Color levelFinal_baseColor;
    Color level1_chosenColor;
    Color level2_chosenColor;
    Color level3_chosenColor;
    Color level4_chosenColor;
    Color levelFinal_chosenColor;

    public int selectedStateY;
    public int selectedStateZ;
    public int selectedMenu;

    public float transparency;
    public float timer;
    public float latency;
    public float sliderSensitivity;
    public float scrollSensitivity;

    float transformEclipse;

    Vector2 move;

    public string moveX;
    public string moveY;

    public string submitString;
    public string cancelString;

    bool jumpButton;
    bool interactButton;

    private void Start()
    {
        oscMessage = oscMaster.GetComponent<ReceivePosition>();

        play_Image = play.GetComponent<Image>();
        eclypse_Image = eclypse.GetComponent<Image>();
        options_Image = options.GetComponent<Image>();
        exit_Image = exit.GetComponent<Image>();
        back_Image = back.GetComponent<Image>();
        volume_Image = volume.GetComponent<Image>();
        music_Image = music.GetComponent<Image>();
        commands_Image = commands.GetComponent<Image>();
        level1_Image = level1.GetComponent<Image>();
        level2_Image = level2.GetComponent<Image>();
        level3_Image = level3.GetComponent<Image>();
        level4_Image = level4.GetComponent<Image>();
        levelFinal_Image = levelFinal.GetComponent<Image>();

        play_Text = play.transform.GetChild(0).GetComponent<TextMesh>();
        eclypse_Text = play.transform.GetChild(0).GetComponent<TextMesh>();
        options_Text = options.transform.GetChild(0).GetComponent<TextMesh>();
        exit_Text = exit.transform.GetChild(0).GetComponent<TextMesh>();
        back_Text = back.transform.GetChild(0).GetComponent<TextMesh>();
        volume_Text = volume.transform.GetChild(0).GetComponent<TextMesh>();
        music_Text = music.transform.GetChild(0).GetComponent<TextMesh>();
        commands_Text = commands.transform.GetChild(0).GetComponent<TextMesh>();

        play_baseColor = play_Image.color;
        play_chosenColor = play_Image.color;
        play_chosenColor.a = transparency;

        eclypse_baseColor = eclypse_Image.color;
        eclypse_chosenColor = eclypse_Image.color;
        eclypse_chosenColor.a = transparency;

        options_baseColor = options_Image.color;
        options_chosenColor = options_Image.color;
        options_chosenColor.a = transparency;

        exit_baseColor = exit_Image.color;
        exit_chosenColor = exit_Image.color;
        exit_chosenColor.a = transparency;

        back_baseColor = back_Image.color;
        back_chosenColor = back_Image.color;
        back_chosenColor.a = transparency;

        volume_baseColor = volume_Image.color;
        volume_chosenColor = volume_Image.color;
        volume_chosenColor.a = transparency;

        music_baseColor = music_Image.color;
        music_chosenColor = music_Image.color;
        music_chosenColor.a = transparency;

        commands_baseColor = commands_Image.color;
        commands_chosenColor = commands_Image.color;
        commands_chosenColor.a = transparency;

        level1_baseColor = level1_Image.color;
        level2_baseColor = level2_Image.color;
        level3_baseColor = level3_Image.color;
        level4_baseColor = level4_Image.color;
        levelFinal_baseColor = levelFinal_Image.color;

        level1_chosenColor = level1_Image.color;
        level2_chosenColor = level2_Image.color;
        level3_chosenColor = level3_Image.color;
        level4_chosenColor = level4_Image.color;
        levelFinal_chosenColor = levelFinal_Image.color;

        level1_chosenColor.a = transparency;
        level2_chosenColor.a = transparency;
        level3_chosenColor.a = transparency;
        level4_chosenColor.a = transparency;
        levelFinal_chosenColor.a = transparency;

        transformEclipse = eclypse_Menu.transform.position.y;

        jumpButton = false;
        interactButton = false;
    }

    private void Update()
    {
        move.x = Input.GetAxis(moveX);
        move.y = Input.GetAxis(moveY);

        if(move == Vector2.zero)
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

            if (Input.GetButton(cancelString) || interactButton == true)
            {
                selectedStateZ--;

                timer = 0;
            }
        }

        


        if (selectedStateZ == 0)
        {

            startMenu.SetActive(true);
            eclypse_Menu.SetActive(false);
            options_Menu.SetActive(false);
            back.SetActive(false);
            levels_Menu.SetActive(false);

            if (selectedStateY == 0)
            {
                play_Image.color = play_chosenColor;
                eclypse_Image.color = eclypse_baseColor;
                options_Image.color = options_baseColor;
                exit_Image.color = exit_baseColor;

                if (timer >= latency)
                {
                    if (Input.GetButton(submitString) || jumpButton == true)
                    {

                        selectedStateZ++;
                        selectedMenu = 0;
                        selectedStateY = 1;

                        levels_Menu.SetActive(true);
                        back.SetActive(true);
                        startMenu.SetActive(false);
                        eclypse_Menu.SetActive(false);
                        options_Menu.SetActive(false);
                        timer = 0;
                    }
                }
            }
            else if (selectedStateY == 1)
            {
                play_Image.color = play_baseColor;
                eclypse_Image.color = eclypse_chosenColor;
                options_Image.color = options_baseColor;
                exit_Image.color = exit_baseColor;

                if (timer >= latency)
                {
                    if (Input.GetButton(submitString) || jumpButton == true)
                    {
                        selectedStateZ++;
                        
                        startMenu.SetActive(false);
                        eclypse_Menu.SetActive(true);
                        back.SetActive(true);
                        levels_Menu.SetActive(false);

                        selectedMenu = 1;
                        selectedStateY = 0;

                        timer = 0;

                    }
                }
            }
            else if (selectedStateY == 2)
            {
                play_Image.color = play_baseColor;
                eclypse_Image.color = eclypse_baseColor;
                options_Image.color = options_chosenColor;
                exit_Image.color = exit_baseColor;

                if (timer >= latency)
                {
                    if (Input.GetButton(submitString) || jumpButton == true)
                    {
                        selectedStateZ++;


                        startMenu.SetActive(false);
                        options_Menu.SetActive(true);
                        back.SetActive(true);
                        levels_Menu.SetActive(false);

                        selectedMenu = 2;
                        selectedStateY = 0;

                        timer = 0;
                    }
                }
            }
            else if (selectedStateY == 3)
            {
                play_Image.color = play_baseColor;
                eclypse_Image.color = eclypse_baseColor;
                options_Image.color = options_baseColor;
                exit_Image.color = exit_chosenColor;

                if (timer >= latency)
                {
                    if (Input.GetButton(submitString) || jumpButton == true)
                    {
                        Application.Quit();

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
        else if(selectedStateZ == 1)
        {
            if(selectedMenu == 0)
            {

                if(selectedStateY == 1)
                {

                    level1_Image.color = level1_chosenColor;
                    level2_Image.color = level2_baseColor;
                    level3_Image.color = level3_baseColor;
                    level4_Image.color = level4_baseColor;
                    levelFinal_Image.color = levelFinal_baseColor;
                    back_Image.color = back_baseColor;
                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString) || jumpButton == true)
                        {
                            SceneManager.LoadScene(1);
                        }
                    }
                }
                else if (selectedStateY == 2)
                {

                    level1_Image.color = level1_baseColor;
                    level2_Image.color = level2_chosenColor;
                    level3_Image.color = level3_baseColor;
                    level4_Image.color = level4_baseColor;
                    levelFinal_Image.color = levelFinal_baseColor;
                    back_Image.color = back_baseColor; 
                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString) || jumpButton == true)
                        {
                            SceneManager.LoadScene(2);
                        }
                    }
                }
                else if (selectedStateY == 3)
                {

                    level1_Image.color = level1_baseColor;
                    level2_Image.color = level2_baseColor;
                    level3_Image.color = level3_chosenColor;
                    level4_Image.color = level4_baseColor;
                    levelFinal_Image.color = levelFinal_baseColor;
                    back_Image.color = back_baseColor; 
                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString) || jumpButton == true)
                        {
                            SceneManager.LoadScene(3);
                        }
                    }
                }
                else if (selectedStateY == 4)
                {

                    level1_Image.color = level1_baseColor;
                    level2_Image.color = level2_baseColor;
                    level3_Image.color = level3_baseColor;
                    level4_Image.color = level4_chosenColor;
                    levelFinal_Image.color = levelFinal_baseColor;
                    back_Image.color = back_baseColor;
                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString) || jumpButton == true)
                        {
                            SceneManager.LoadScene(4);
                        }
                    }
                }
                else if (selectedStateY == 5)
                {

                    level1_Image.color = level1_baseColor;
                    level2_Image.color = level2_baseColor;
                    level3_Image.color = level3_baseColor;
                    level4_Image.color = level4_baseColor;
                    levelFinal_Image.color = levelFinal_chosenColor;
                    back_Image.color = back_baseColor;
                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString) || jumpButton == true)
                        {
                            SceneManager.LoadScene(5);
                        }
                    }
                }
                else if (selectedStateY == 6)
                {
                    level1_Image.color = level1_baseColor;
                    level2_Image.color = level2_baseColor;
                    level3_Image.color = level3_baseColor;
                    level4_Image.color = level4_baseColor;
                    levelFinal_Image.color = levelFinal_baseColor;
                    back_Image.color = back_chosenColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString) || jumpButton == true)
                        {
                            selectedStateZ--;

                            timer = 0;
                        }
                    }
                }
                else if(selectedStateY >= 7)
                {
                    selectedStateY = 1;
                }
                else if(selectedStateY <= 0)
                {
                    selectedStateY = 6;
                }


                if (timer >= latency)
                {
                    if (Input.GetButton(submitString) || jumpButton == true)
                    {
                        selectedStateZ--;

                        timer = 0;
                    }
                }
            }
            else if(selectedMenu == 1)
            {
                back_Image.color = back_chosenColor;

                transformEclipse += -move.y * Time.unscaledDeltaTime * scrollSensitivity;

                eclypse_Menu.transform.position = new Vector3(eclypse.transform.position.x, transformEclipse, eclypse.transform.position.z);

                if (timer >= latency)
                {
                    if (Input.GetButton(submitString) || jumpButton == true)
                    {
                        selectedStateZ--;

                        timer = 0;
                    }
                }
            }
            else if (selectedMenu == 2)
            {
                if (selectedStateY == 0)
                {
                    volume_Image.color = volume_baseColor;
                    commands_Image.color = commands_baseColor;
                    back_Image.color = back_baseColor;
                    music_Image.color = music_chosenColor;

                    audioSlider2.GetComponent<Slider>().value += (move.x * sliderSensitivity * Time.unscaledDeltaTime);
                }
                else if(selectedStateY == 1)
                {
                    volume_Image.color = volume_chosenColor;
                    commands_Image.color = commands_baseColor;
                    back_Image.color = back_baseColor;
                    music_Image.color = music_baseColor;

                    audioSlider.GetComponent<Slider>().value += (move.x * sliderSensitivity * Time.unscaledDeltaTime);
                }
                else if(selectedStateY == 2)
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
                            levels_Menu.SetActive(false);

                            timer = 0;
                        }
                        else
                        {
                            commandsImage.SetActive(false);
                            options_Menu.SetActive(true);
                            back.SetActive(true);
                            levels_Menu.SetActive(false);
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
                        if (Input.GetButton(submitString) || jumpButton == true)
                        {
                            selectedStateZ--;

                            timer = 0;
                        }
                    }
                }
                else if(selectedStateY >= 4)
                {
                    selectedStateY = 0;
                }
                else if (selectedStateY <= -1)
                {
                    selectedStateY = 3;
                }
            }
        }
        else if(selectedStateZ <= -1)
        {
            selectedStateZ = 0;
        }
    }


}
