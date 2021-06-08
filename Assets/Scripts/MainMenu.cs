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
    public GameObject audioSlider;
    public GameObject commands;

    Image play_Image;
    Image eclypse_Image;
    Image options_Image;
    Image exit_Image;
    Image back_Image;
    Image volume_Image;
    Image commands_Image;

    TextMesh play_Text;
    TextMesh eclypse_Text;
    TextMesh options_Text;
    TextMesh exit_Text;
    TextMesh back_Text;
    TextMesh options_Menu_Text;
    TextMesh volume_Text;
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
    Color commands_baseColor;
    Color commands_chosenColor;

    public int selectedStateY;
    public int selectedStateZ;
    public int selectedMenu;

    public float transparency;
    public float timer;
    public float latency;
    public float sliderSensitivity;

    Vector2 move;

    public string moveX;
    public string moveY;

    public string submitString;
    public string cancelString;

    private void Start()
    {
        oscMessage = oscMaster.GetComponent<ReceivePosition>();

        play_Image = play.GetComponent<Image>();
        eclypse_Image = eclypse.GetComponent<Image>();
        options_Image = options.GetComponent<Image>();
        exit_Image = exit.GetComponent<Image>();
        back_Image = back.GetComponent<Image>();
        volume_Image = volume.GetComponent<Image>();
        commands_Image = commands.GetComponent<Image>();

        play_Text = play.transform.GetChild(0).GetComponent<TextMesh>();
        eclypse_Text = play.transform.GetChild(0).GetComponent<TextMesh>();
        options_Text = options.transform.GetChild(0).GetComponent<TextMesh>();
        exit_Text = exit.transform.GetChild(0).GetComponent<TextMesh>();
        back_Text = back.transform.GetChild(0).GetComponent<TextMesh>();
        volume_Text = volume.transform.GetChild(0).GetComponent<TextMesh>();
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

        commands_baseColor = commands_Image.color;
        commands_chosenColor = commands_Image.color;
        commands_chosenColor.a = transparency;
    }

    private void Update()
    {
        move.x = Input.GetAxis(moveX);
        move.y = Input.GetAxis(moveY);

        //move.x = -(oscMessage.xAxis_ );
        //move.y = oscMessage.zAxis_;

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

            startMenu.SetActive(true);
            eclypse_Menu.SetActive(false);
            options_Menu.SetActive(false);
            back.SetActive(false);

            if (selectedStateY == 0)
            {
                play_Image.color = play_chosenColor;
                eclypse_Image.color = eclypse_baseColor;
                options_Image.color = options_baseColor;
                exit_Image.color = exit_baseColor;

                if (timer >= latency)
                {
                    if (Input.GetButton(submitString))
                    {
                        SceneManager.LoadScene(1);

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
                    if (Input.GetButton(submitString))
                    {
                        selectedStateZ++;

                        startMenu.SetActive(false);
                        eclypse_Menu.SetActive(true);
                        back.SetActive(true);

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
                    if (Input.GetButton(submitString))
                    {
                        selectedStateZ++;


                        startMenu.SetActive(false);
                        options_Menu.SetActive(true);
                        back.SetActive(true);

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
                    if (Input.GetButton(submitString))
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
                selectedStateY = 2;
            }

        }
        else if(selectedStateZ == 1)
        {
            if(selectedMenu == 1)
            {
                back_Image.color = back_chosenColor;

                if (timer >= latency)
                {
                    if (Input.GetButton(submitString))
                    {
                        selectedStateZ--;

                        timer = 0;
                    }
                }
            }
            else if (selectedMenu == 2)
            {
                if(selectedStateY == 0)
                {
                    volume_Image.color = volume_chosenColor;
                    commands_Image.color = commands_baseColor;
                    back_Image.color = back_baseColor;

                    audioSlider.GetComponent<Slider>().value += (move.x * sliderSensitivity * Time.unscaledDeltaTime);
                }
                else if(selectedStateY == 1)
                {
                    volume_Image.color = volume_baseColor;
                    commands_Image.color = commands_chosenColor;
                    back_Image.color = back_baseColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString))
                        {
                            Debug.Log("Montre les commandes");

                            timer = 0;
                        }
                    }
                }
                else if (selectedStateY == 2)
                {
                    volume_Image.color = volume_baseColor;
                    commands_Image.color = commands_baseColor;
                    back_Image.color = back_chosenColor;

                    if (timer >= latency)
                    {
                        if (Input.GetButton(submitString))
                        {
                            selectedStateZ--;

                            timer = 0;
                        }
                    }
                }
                else if(selectedStateY >= 3)
                {
                    selectedStateY = 0;
                }
                else if (selectedStateY <= -1)
                {
                    selectedStateY = 2;
                }
            }
        }
        else if(selectedStateZ <= -1)
        {
            selectedStateZ = 0;
        }
    }


}
