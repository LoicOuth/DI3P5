import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { LayoutRoutingModule } from './layout-routing.module'
import { LayoutComponent } from './layout.component'
import { SharedModule } from '../shared/shared.module'
import { NavLinkComponent } from '../components/nav-link/nav-link.component'
import { ClickOutsideModule } from 'ng-click-outside'
import { PropertiesNavbarComponent } from '../components/properties-navbar/properties-navbar.component'
import { ManageBlocksComponent } from '../components/manage-blocks/manage-blocks.component'
import { FontSizeComponent } from '../components/properties-navbar/tab-content/font-size/font-size.component'
import { TextDecorationComponent } from '../components/properties-navbar/tab-content/text-decoration/text-decoration.component'
import { ChooseColorComponent } from '../components/properties-navbar/tab-style/choose-color/choose-color.component'
import { TabContentComponent } from '../components/properties-navbar/tab-content/tab-content.component'
import { TabStyleComponent } from '../components/properties-navbar/tab-style/tab-style.component'
import { AlignTextComponent } from '../components/properties-navbar/tab-content/align-text/align-text.component'
import { AddContentComponent } from '../components/add-content/add-content.component'
import { EnumToArrayPipe } from '../core/pipe/enum-to-array.pipe'
import { TabAdvancedComponent } from '../components/properties-navbar/tab-advanced/tab-advanced.component'
import { FlexEditComponent } from '../components/properties-navbar/tab-content/flex-edit/flex-edit.component'
import { AddPageComponent } from '../components/add-page/add-page.component'
import { AddBlockComponent } from '../components/add-block/add-block.component'
import { ContentItemComponent } from '../components/content-item/content-item.component'
import { SizeEditComponent } from '../components/properties-navbar/tab-style/size-edit/size-edit.component'
import { SpacingEditComponent } from '../components/properties-navbar/tab-style/spacing-edit/spacing-edit.component'
import { PageDetailComponent } from '../components/page-detail/page-detail.component'
import { AddMenuComponent } from '../components/add-menu/add-menu.component'
import { ManageMenuComponent } from '../components/manage-menu/manage-menu.component'
import { BoxShadowComponent } from '../components/properties-navbar/tab-style/box-shadow/box-shadow.component'
import { ContentDirectionComponent } from '../components/properties-navbar/tab-content/content-direction/content-direction.component'
import { PreviewPageComponent } from '../components/preview-page/preview-page.component'
import { ManageTemplateComponent } from '../components/manage-template/manage-template.component'
import { SelectFileComponent } from '../components/properties-navbar/tab-content/select-file/select-file.component'

@NgModule({
  declarations: [
    EnumToArrayPipe,

    LayoutComponent,
    NavLinkComponent,
    ManageBlocksComponent,
    AddContentComponent,
    PropertiesNavbarComponent,
    AddBlockComponent,
    FontSizeComponent,
    AlignTextComponent,
    TextDecorationComponent,
    ChooseColorComponent,
    TabContentComponent,
    TabStyleComponent,
    TabAdvancedComponent,
    FlexEditComponent,
    SizeEditComponent,
    ContentItemComponent,
    AddPageComponent,
    SpacingEditComponent,
    PageDetailComponent,
    AddMenuComponent,
    ManageMenuComponent,
    BoxShadowComponent,
    ContentDirectionComponent,
    ManageTemplateComponent,
    SelectFileComponent,
    PreviewPageComponent,
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    SharedModule,
    ClickOutsideModule
  ],
})
export class LayoutModule { }
