use std::env;

fn main() {
    let image_path = env::args().nth(1).expect("No image path provided.");

    let img = image::open(&image_path).expect("No image found at provided path");

    let rgb_img = img.to_rgba();

    println!(
        "Using image file: {}, width {}, height {}",
        image_path,
        rgb_img.width(),
        rgb_img.height()
    );

    let img_blurred = imageproc::filter::gaussian_blur_f32(&rgb_img, 5.0);
    imageproc::window::display_multiple_images("image", &[&rgb_img, &img_blurred], 800, 600);

    let path = std::path::Path::new(&image_path);
    let file_name = path.file_name().unwrap().to_str().unwrap();
    let gray_file_path = path.with_file_name(format!("gray-{}", file_name));

    let gray_img = img.to_luma();
    gray_img
        .save(gray_file_path)
        .expect("Could not save grayscale image")
}
