using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageContainerScript : MonoBehaviour
{
    public List<Sprite> imageList = new List<Sprite>();
    public int numberOfObjectsToSpawn = 10;
    public GameObject tilePrefab;

    private int currentImageIndex = 0;
    private GameObject imageObject;
    public Rigidbody2D rb;
    private bool mouseClicked = false;
    void Start()
    {


        StartCoroutine(SpawnRandomImages(0f));

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            mouseClicked = true;
            rb = imageObject.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = imageObject.AddComponent<Rigidbody2D>();
            }

            rb.gravityScale = 1f;
            StartCoroutine(SpawnRandomImages(0.3f));
        }
    }

    IEnumerator SpawnRandomImages(float delay)
    {

        yield return new WaitForSeconds(delay);
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            imageObject = Instantiate(tilePrefab, new Vector3(-0.04f, 2.61f, 0.01707233f), Quaternion.identity, transform);
            int randomIndex = Random.Range(0, imageList.Count);
            Sprite randomImage = imageList[randomIndex];

            SpriteRenderer spriteRenderer = imageObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = imageObject.AddComponent<SpriteRenderer>();
            }
            spriteRenderer.sprite = randomImage;
        }

        // Sau khi hình rơi xuống, gọi hàm ChangeImageAndPosition để thay đổi hình ảnh và vị trí

    }

    // void ChangeImageAndPosition()
    // {

    //     // Tăng chỉ số của hình ảnh hiện tại hoặc đặt lại nếu đã là hình cuối cùng trong danh sách
    //     currentImageIndex = (currentImageIndex + 1) % imageList.Count;

    //     // In ra thông điệp để kiểm tra xem hình ảnh đã thay đổi
    //     Debug.Log("Changed to image index: " + currentImageIndex);
    // }
}
