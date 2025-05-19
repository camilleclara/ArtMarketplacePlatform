import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { Gallery, GalleryItem, ImageItem, ImageSize, ThumbnailsPosition } from 'ng-gallery';
import { Lightbox, LightboxModule } from 'ng-gallery/lightbox';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, LightboxModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  title = 'frontend-app';
  galleryId = 'myLightbox';
  items: GalleryItem[] = [];
  imageData = [
    {
      srcUrl: 'https://preview.ibb.co/jrsA6R/img12.jpg',
      previewUrl: 'https://preview.ibb.co/jrsA6R/img12.jpg',
    },
    {
      srcUrl: 'https://preview.ibb.co/kPE1D6/clouds.jpg',
      previewUrl: 'https://preview.ibb.co/kPE1D6/clouds.jpg',
    },
    {
      srcUrl: 'https://preview.ibb.co/mwsA6R/img7.jpg',
      previewUrl: 'https://preview.ibb.co/mwsA6R/img7.jpg',
    },
    {
      srcUrl: 'https://preview.ibb.co/kZGsLm/img8.jpg',
      previewUrl: 'https://preview.ibb.co/kZGsLm/img8.jpg',
    },
  ];
  constructor(public gallery: Gallery, public lightbox: Lightbox){
    
  }
  ngOnInit() {
    // Load items into gallery
    // const galleryRef = this.gallery.ref(this.galleryId);
    // galleryRef.load(this.items);
     // Creat gallery items
     this.items = this.imageData.map(
      (item) => new ImageItem({ src: item.srcUrl, thumb: item.previewUrl })
    );

    /** Lightbox Example */

    // Get a lightbox gallery ref
    const lightboxRef = this.gallery.ref('lightbox');

    // Add custom gallery config to the lightbox (optional)
    lightboxRef.setConfig({
      imageSize: ImageSize.Cover,
      thumbPosition: ThumbnailsPosition.Top,
      thumbs: true,
      counter: true,
    });

    // Load items into the lightbox gallery ref
    lightboxRef.load(this.items);
  }
}
